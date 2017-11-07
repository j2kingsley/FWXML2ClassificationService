using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Net.Http;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using Newtonsoft.Json;
using Npgsql;

namespace XML2Table
{
    public partial class Xml2Db : Form
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static bool includeLogs = false;
        private string xmlFilePath = null;
        private static dynamic queryCsv = null;

        private static List<string> taCategoryRegistry = null;
        private static List<string> taAttributeRegistry = null;
        private static List<string> zoneCategoryRegistry = null;
        private static List<string> zoneAttributeRegistry = null;

        private static long taSequence = 10001;
        private static long zoneSequence = 20001;

        private static long taAttributeSequence = 30001;
        private static long zoneAttributeSequence = 40001;

        public Xml2Db()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openXmlFileDialog.ShowDialog();
            txtBox.Text = openXmlFileDialog.FileName.ToString();
            xmlFilePath = openXmlFileDialog.FileName.ToString();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (xmlFilePath == null)
                {
                    lblNewStatus.Text = "Select XML File!";
                }
                else
                {
                    //Include logs
                    if (checkBx.Checked)
                    {
                        includeLogs = true;
                    }

                    //Read and Store CSV Data
                    ReadCsvFile();

                    //Process XML
                    processXml(xmlFilePath, lblNewStatus);

                    //ReadXmlAndInsert2Db(xmlFilePath);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                log.Info(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
                log.Debug(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
            }
        }

        private static void ReadCsvFile()
        {
            try
            {

                string[] allLines = File.ReadAllLines(ConfigurationManager.AppSettings["csvFilePath"]);

                queryCsv = from line in allLines
                    let data = line.Split(',')
                    select new
                    {
                        RatingAuthId = data[0],
                        TACode = data[1]

                    };
            }
            catch (Exception ex)
            {
                log.Debug(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
            }
        }

        private static int TaCodeValue(int ratingAuthorityID)
        {
            int taCode = 0;
            try
            {
                foreach (var eachSet in queryCsv)
                {
                    if (eachSet.RatingAuthId == ratingAuthorityID.ToString())
                    {
                        taCode = Utils.SafeInt(eachSet.TACode);
                        break;
                    }
                }

                return taCode;
            }
            catch (Exception ex)
            {

                log.Debug(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
                return taCode;
            }
        }

        private static string EscapeSingleQuote(string escapeQuote)
        {
            string returnValue = null;
            try
            {

                if (escapeQuote != null)
                {
                    returnValue = escapeQuote.Replace("'", "''");
                }


                return returnValue;
            }
            catch (Exception ex)
            {

                log.Debug(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
                return returnValue;
            }
        }

        private static XElement GetXElement(XElement ele)
        {
            if (ele == null)
            {
                return null;
            }
            return ele;
        }

        private static string CreateCategory(string inputValue)
        {
            string categoryName = null;
            try
            {
                if (inputValue != null)
                {
                    if (inputValue.Split(' ').Length <= 5)
                    {
                        categoryName = Regex.Replace(inputValue, @"[^0-9a-zA-Z]+", "");
                    }
                    else
                    {
                        var counter = 0;
                        var codeLength = inputValue.Split(' ').Length;

                        foreach (var eachWord in inputValue.Split(' '))
                        {
                            counter = counter + 1;

                            var lclWord = eachWord.FirstOrDefault().ToString().ToUpper();

                            //Too avoid duplicates concatenating last char
                            if (codeLength == counter)
                            {
                                lclWord = lclWord + eachWord.LastOrDefault().ToString().ToUpper();
                            }

                            categoryName = categoryName + lclWord;

                        }
                        if (categoryName != null)
                        {
                            categoryName = Regex.Replace(categoryName, @"[^0-9a-zA-Z]+", "");

                        }

                    }


                    //categoryName = Regex.Replace(inputValue, @"[^0-9a-zA-Z]+", "");


                }

                return categoryName;
            }
            catch (Exception ex)
            {

                log.Debug(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
                return categoryName;
            }
        }

        private static string CreateCode(string inputValue)
        {
            //if (inputValue == "7-Residential 6A.  Auckland City - Isthmus Section 199910")
            //{
            //    Console.WriteLine("Stop");
            //}

            string codeName = null;
            try
            {
                if (inputValue != null)
                {
                    if (inputValue.Split(' ').Length <= 3)
                    {
                        codeName = Regex.Replace(inputValue, @"[^0-9a-zA-Z]+", "");
                    }
                    else
                    {
                        var counter = 0;
                        var codeLength = inputValue.Split(' ').Length;

                        foreach (var eachWord in inputValue.Split(' '))
                        {
                            counter = counter + 1;

                            var lclWord = eachWord.FirstOrDefault().ToString().ToUpper();

                            //Too avoid duplicates concatenating last char
                            if (codeLength == counter)
                            {
                                lclWord = lclWord + eachWord.LastOrDefault().ToString().ToUpper();
                            }

                            codeName = codeName + lclWord;

                        }
                        if (codeName != null)
                        {
                            codeName = Regex.Replace(codeName, @"[^0-9a-zA-Z]+", "");

                        }

                    }

                }

                return codeName;
            }
            catch (Exception ex)
            {

                log.Debug(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
                return codeName;
            }
        }

        private static void processXml(string xmlFilePath, Label lblStatus)
        {
            try
            {
                taCategoryRegistry = new List<string>();
                zoneCategoryRegistry = new List<string>();

                taAttributeRegistry = new List<string>();
                zoneAttributeRegistry = new List<string>();

                XElement xelement = XElement.Load(xmlFilePath);
                IEnumerable<XElement> classfications = xelement.Descendants("List_Item");

                int listCounter = 0;
                int totalRowCounter = 0;

                string grpCategory = null;
                string grpCode = null;
                string grpDescription = null;

                foreach (var eachClassification in classfications)
                {
                    //INSERT INTO classification.classification (category, code, description, short_description, active, parent_category, parent_code, group_category, group_code) 
                    //VALUES ('TAZoning63', 'APZ', 'Aquatic Park Zone', null, true, 'TAZoning', '63');

                    //1. Get Category Name [Getting Group name and details]
                    if (listCounter == 0)
                    {
                        if (eachClassification.Parent.HasAttributes)
                        {
                            grpCategory = eachClassification.Parent.FirstAttribute.Value.Replace(" ", "");

                            log.Info("-- Create " + eachClassification.Parent.FirstAttribute.Value + " classification");

                            if (includeLogs)
                            {
                                Console.WriteLine("Group Name: " + eachClassification.Parent.FirstAttribute.Value);
                                log.Info("Group Name: " + eachClassification.Parent.FirstAttribute.Value);

                                Console.WriteLine("Group Name: " + eachClassification.Parent.LastAttribute.Value);
                            }

                            foreach (var eachGroupAttrbs in eachClassification.Parent.Attributes())
                            {
                                if (eachGroupAttrbs.Name.LocalName == "name")
                                {
                                    grpCategory = CreateCategory(eachGroupAttrbs.Value);

                                    foreach (var eachWord in eachGroupAttrbs.Value.Split(' '))
                                    {
                                        string lclCode = eachWord.FirstOrDefault().ToString().ToUpper();
                                        grpCode = grpCode + lclCode;
                                    }

                                }
                                else if (eachGroupAttrbs.Name.LocalName == "description")
                                {
                                    grpDescription = eachGroupAttrbs.Value;
                                }


                            }

                            totalRowCounter = totalRowCounter + 1;
                            string insertGrpQuery =
                                "INSERT INTO classification.classification (category, code, description, short_description, active, parent_category, parent_code, group_category, group_code) VALUES (" +
                                "'" + grpCategory + "', '" + grpCode + "', '" + EscapeSingleQuote(grpDescription) +
                                "', null, " + true + ", null, null, '" + grpCategory + "', '" + grpCode + "');";
                            log.Info("		" + insertGrpQuery);
                        }
                    }

                    //2. Get Parent List name [Getting TA Lists]
                    if (eachClassification.Parent.Name.LocalName == "List")
                    {
                        string prtCategory = null;
                        string prtCode = null;
                        string prtDescription = null;
                        string prtParentCategory = null;
                        string prtParentCode = null;
                        string prtShortDescription = null;

                        listCounter = listCounter + 1;

                        if (includeLogs)
                        {
                            log.Info("                                                                              ");
                            log.Info("------------------------------------------------------------------------------");
                            log.Info("                                                                              ");

                            log.Info("List Counter: " + listCounter.ToString());
                        }

                        if (GetXElement(eachClassification.Element("Backend_ID")) != null)
                        {
                            //Get TA Code
                            int taCode = TaCodeValue(Utils.SafeInt(eachClassification.Element("Backend_ID").Value));

                            if (includeLogs)
                            {
                                Console.WriteLine(eachClassification.Element("Backend_ID").Value);
                                log.Info("		Parent Backend_ID: " + eachClassification.Element("Backend_ID").Value +
                                         "		TA Code: " + taCode.ToString());
                            }

                            prtCode = taCode == 0 ? eachClassification.Element("Backend_ID").Value : taCode.ToString();
                            prtCode = Regex.Replace(prtCode, @"[^0-9a-zA-Z]+", "");

                        }

                        if (GetXElement(eachClassification.Element("Value")) != null)
                        {
                            if (includeLogs)
                            {
                                Console.WriteLine(eachClassification.Element("Value").Value);
                                log.Info("		Parent Value: " + eachClassification.Element("Value").Value);
                            }
                            prtShortDescription = eachClassification.Element("Value").Value;
                            prtCategory = CreateCategory(eachClassification.Element("Value").Value);

                            if (taCategoryRegistry.Contains(prtCategory))
                            {
//Then it is dupicate
                                taSequence = taSequence + 1;
                                prtCategory = prtCategory + taSequence;
                            }
                            else
                            {
                                taCategoryRegistry.Add(prtCategory);
                            }


                        }

                        //Status:
                        lblStatus.Text = "Processing: " + listCounter.ToString() + " - " + prtCategory;

                        if (GetXElement(eachClassification.Element("Description")) != null)
                        {
                            if (includeLogs)
                            {
                                Console.WriteLine(eachClassification.Element("Description").Value);
                                log.Info("		Parent Description: " + eachClassification.Element("Description").Value);
                            }

                            prtDescription = eachClassification.Element("Description").Value;
                        }

                        prtParentCategory = grpCategory;
                        prtParentCode = grpCode;

                        totalRowCounter = totalRowCounter + 1;
                        var insertParentQuery =
                            "INSERT INTO classification.classification (category, code, description, short_description, active, parent_category, parent_code, group_category, group_code) VALUES (" +
                            "'" + prtCategory + "', '" + prtCode + "', '" + EscapeSingleQuote(prtDescription) + "', '" +
                            EscapeSingleQuote(prtShortDescription) + "', " + true + ", '" + prtParentCategory + "', '" +
                            prtParentCode + "', '" + grpCategory + "', '" + grpCode + "');";
                        log.Info("		" + insertParentQuery);


                        //Parents(TA) Attributes
                        IEnumerable<XElement> attributesChildList = from eA in eachClassification.Elements("Attribute")
                            select eA;


                        foreach (XElement parentAttrValue in attributesChildList)
                        {
                            if (includeLogs)
                            {
                                Console.WriteLine(parentAttrValue.Value);
                                log.Info("		Parent Attribute: " + parentAttrValue.Value);
                            }
                            string ptrAttributeCategory = null;
                            string ptrAttributeCode = null;
                            string ptrAttributeDescription = null;
                            string ptrAttributeParentCategory = null;
                            string ptrAttributeParentCode = null;



                            ptrAttributeCategory = "Attributes" + prtCategory;
                            //ptrAttributeCategory = "Attributes";
                            if (taAttributeRegistry.Contains(ptrAttributeCategory))
                            {
//Then it is dupicate
                                taAttributeSequence = taAttributeSequence + 1;
                                ptrAttributeCategory = ptrAttributeCategory + taAttributeSequence;
                            }
                            else
                            {
                                taAttributeRegistry.Add(ptrAttributeCategory);
                            }

                            ptrAttributeCode = taAttributeSequence.ToString();
                            ptrAttributeDescription = parentAttrValue.Value;
                            ptrAttributeParentCategory = prtCategory;
                            ptrAttributeParentCode = prtCode;

                            totalRowCounter = totalRowCounter + 1;
                            var insertptrAttributesQuery =
                                "INSERT INTO classification.classification (category, code, description, short_description, active, parent_category, parent_code, group_category, group_code) VALUES (" +
                                "'" + ptrAttributeCategory + "', '" + ptrAttributeCode + "', '" +
                                EscapeSingleQuote(ptrAttributeDescription) + "', null, " + true + ", '" +
                                ptrAttributeParentCategory + "', '" + ptrAttributeParentCode + "', '" + grpCategory +
                                "', '" + grpCode + "');";
                            log.Info("		" + insertptrAttributesQuery);

                        }


                        //3. Get Children List [Gettting ZONE Lists]
                        IEnumerable<XElement> elementsChildList = from eA in eachClassification.Elements("List_Item")
                            select eA;
                        int zoneBackendIdcounter = 0;
                        foreach (XElement eleValue in elementsChildList)
                        {
                            zoneBackendIdcounter = zoneBackendIdcounter + 1;

                            string childCategory = null;
                            string childCode = null;
                            string childDescription = null;
                            string childParentCategory = null;
                            string childParentCode = null;
                            string childShortDescription = null;

                            if (GetXElement(eleValue.Element("Backend_ID")) != null)
                            {
                                //Get TA Code
                                int taCode = TaCodeValue(Utils.SafeInt(eleValue.Element("Backend_ID").Value));
                                if (includeLogs)
                                {
                                    Console.WriteLine(eleValue.Element("Backend_ID").Value);
                                    log.Info("		Backend_ID: " + eleValue.Element("Backend_ID").Value + "		TA Code: " +
                                             taCode.ToString());

                                }
                                childCode = taCode == 0 ? eleValue.Element("Backend_ID").Value : taCode.ToString();

                                //Someback end ID has long string values :(
                                childCode = CreateCode(childCode);

                                //Found out that for some zones Backend_ID is null. So concatenating parent code and randomly generated number for code
                                if (childCode == "" || childCode == string.Empty || childCode == null)
                                {
                                    childCode = prtCode + zoneBackendIdcounter.ToString();
                                }
                                childCode = childCode + zoneBackendIdcounter.ToString();

                            }
                            if (GetXElement(eleValue.Element("Value")) != null)
                            {
                                if (includeLogs)
                                {
                                    Console.WriteLine(eleValue.Element("Value").Value);
                                    log.Info("		Value: " + eleValue.Element("Value").Value);
                                }

                                childShortDescription = eleValue.Element("Value").Value;

                                childCategory = CreateCategory(eleValue.Element("Value").Value);
                                //Found some duplicate values in Child Category list
                                //childCategory = childCategory + prtCode;

                                if (zoneCategoryRegistry.Contains(childCategory))
                                {
//Then it is duplicate zone
                                    zoneSequence = zoneSequence + 1;
                                    childCategory = childCategory + zoneSequence;
                                }
                                else
                                {
                                    zoneCategoryRegistry.Add(childCategory);
                                }


                            }


                            if (GetXElement(eleValue.Element("Description")) != null)
                            {
                                if (includeLogs)
                                {
                                    Console.WriteLine(eleValue.Element("Description").Value);
                                    log.Info("		Description: " + eleValue.Element("Description").Value);
                                }

                                childDescription = eleValue.Element("Description").Value;
                            }


                            childParentCategory = prtCategory;
                            childParentCode = prtCode;

                            totalRowCounter = totalRowCounter + 1;
                            var insertChildQuery =
                                "INSERT INTO classification.classification (category, code, description, short_description, active, parent_category, parent_code, group_category, group_code) VALUES (" +
                                "'" + childCategory + "', '" + childCode + "', '" + EscapeSingleQuote(childDescription) +
                                "', '" + EscapeSingleQuote(childShortDescription) + "', " + true + ", '" +
                                childParentCategory + "', '" + childParentCode + "', '" + grpCategory + "', '" + grpCode +
                                "');";
                            log.Info("		" + insertChildQuery);

                            //Children(ZONES) Attributes
                            IEnumerable<XElement> attributeChildList = from eA in eleValue.Elements("Attribute")
                                select eA;

                            foreach (XElement attrValue in attributeChildList)
                            {
                                string attributeCategory = null;
                                string attributeCode = null;
                                string attributeDescription = null;
                                string attributeParentCategory = null;
                                string attributeParentCode = null;


                                if (includeLogs)
                                {
                                    Console.WriteLine(attrValue.Value);
                                    log.Info("		Attribute: " + attrValue.Value);
                                }


                                attributeCategory = "Attributes" + childCategory;
                                //attributeCategory = "Attributes";
                                if (zoneAttributeRegistry.Contains(attributeCategory))
                                {
//Then it is dupicate
                                    zoneAttributeSequence = zoneAttributeSequence + 1;
                                    attributeCategory = attributeCategory + zoneAttributeSequence;
                                }
                                else
                                {
                                    zoneAttributeRegistry.Add(attributeCategory);
                                }

                                attributeCode = zoneAttributeSequence.ToString();
                                attributeDescription = attrValue.Value;
                                attributeParentCategory = childCategory;
                                attributeParentCode = childCode;

                                totalRowCounter = totalRowCounter + 1;
                                var insertAttributesQuery =
                                    "INSERT INTO classification.classification (category, code, description, short_description, active, parent_category, parent_code, group_category, group_code) VALUES (" +
                                    "'" + attributeCategory + "', '" + attributeCode + "', '" +
                                    EscapeSingleQuote(attributeDescription) + "', null, " + true + ", '" +
                                    attributeParentCategory + "', '" + attributeParentCode + "', '" + grpCategory +
                                    "', '" + grpCode + "');";
                                log.Info("		" + insertAttributesQuery);

                            }

                            if (includeLogs)
                            {
                                log.Info(
                                    "                                                                              ");
                                log.Info(
                                    "                                                                              ");

                            }

                        }

                    }

                }

                MessageBox.Show("Processing XML is Completed!");
                lblStatus.Text = "Processing XML is Completed!";
                log.Info("--Total Inserted Rows: " + totalRowCounter.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine(" Exception : " + ex.Message);
                log.Error(MethodBase.GetCurrentMethod().Name + " | " + "Exception: " + ex.Message);
            }
            finally
            {
                queryCsv = null;
                includeLogs = false;

                taCategoryRegistry = null;
                zoneCategoryRegistry = null;
                taAttributeRegistry = null;
                zoneAttributeRegistry = null;

                taSequence = 10001;
                zoneSequence = 20001;
                taAttributeSequence = 30001;
                zoneAttributeSequence = 40001;
            }
        }

        private void getPostgressConnection()
        {
            try
            {
                string dbHost = ConfigurationManager.AppSettings["dbHost"];
                string dbPort = ConfigurationManager.AppSettings["dbPort"];
                string dbName = ConfigurationManager.AppSettings["dbName"];
                string dbUserName = ConfigurationManager.AppSettings["dbUserName"];
                string dbPassword = ConfigurationManager.AppSettings["dbPassword"];


                var connString = "Host=" + dbHost + ";Port=" + dbPort + ";Username=" + dbUserName + ";Password=" +
                                 dbPassword + ";Database=" + dbName;
                Console.WriteLine("connString: ", connString);

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    //// Insert some data
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText =
                            "INSERT INTO classification.classification (category, code, description, short_description, active, parent_category, parent_code, group_category, group_code) VALUES (@category, @code, @description, @short_description, @active, @parent_category, @parent_code)";
                        cmd.Parameters.AddWithValue("@category", "HelloWorld");
                        cmd.Parameters.AddWithValue("@code", "Hello world");
                        cmd.Parameters.AddWithValue("@description", "Hello world");
                        cmd.Parameters.AddWithValue("@short_description", "Hello world");

                        NpgsqlParameter param = new NpgsqlParameter();
                        param.ParameterName = "@active";
                        param.Value = true;
                        param.DbType = System.Data.DbType.Boolean;
                        cmd.Parameters.Add(param);

                        cmd.Parameters.AddWithValue("@parent_category", "Hello world");
                        cmd.Parameters.AddWithValue("@parent_code", "Hello world");
                        cmd.ExecuteNonQuery();
                    }

                    //// Retrieve all rows
                    //string query = "SELECT category, code, description, short_description, active, parent_category, parent_code FROM classification.classification WHERE category = \'SaleType\'";
                    //using (var cmd = new NpgsqlCommand(query, conn))
                    //using (var reader = cmd.ExecuteReader())
                    //    while (reader.Read())
                    //        Console.WriteLine(reader.GetString(2));

                }
            }
            catch (Exception ex)
            {

                log.Debug(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
            }

        }
        private void getPostgressCon()
        {
            try
            {
                string dbHost = ConfigurationManager.AppSettings["dbHost"];
                string dbPort = ConfigurationManager.AppSettings["dbPort"];
                string dbName = ConfigurationManager.AppSettings["dbName"];
                string dbUserName = ConfigurationManager.AppSettings["dbUserName"];
                string dbPassword = ConfigurationManager.AppSettings["dbPassword"];


                var connString = "Host=" + dbHost + ";Port=" + dbPort + ";Username=" + dbUserName + ";Password=" +
                                 dbPassword + ";Database=" + dbName;
                Console.WriteLine("connString: ", connString);

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                  

                }
            }
            catch (Exception ex)
            {

                log.Debug(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
            }

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            //Console.WriteLine(CreateCategory("Minimum Site 280m² with a %building() commitment and 500m² without."));
            GenerateRandomNo();
            //Addmedia();

        }

        public static int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;

            IEnumerable<int> squares = Enumerable.Range(1, 10)
                .Select(x => x);

            foreach (int num in squares)
            {
                Console.WriteLine(num);
            }


            return 0;
        }

        public static async void Addmedia()
        {
            try
            {
                Debug.WriteLine("Addmedia");

                using (var client = new HttpClient())
                {
                    var mediaItem = new MediaItem
                    {
                        id = "1249d6ce-ad65-11e7-abc4-cec278b6b50a",
                        fileName = "FromScript.jpg",
                        contentType = "image/jpeg",
                        status = "UPLOAD_INITIATED",
                        captureDate = "2014-01-02",
                        uploadedDate = "2016-01-02",
                        isIncludedInInsurance = false,
                        isIncludedInInsuranceReports = false
                    };

                    var mediaEntry = new MediaEntry
                    {
                        id = "1249d974-ad65-11e7-abc4-cec278b6b50a",
                        ownerId = "36e20bdd-64a4-4501-9a46-8302cd807104",
                        isPrimary = false,
                        qivsPhotoId = "123_1",
                        mediaItem = mediaItem
                    };

                    var json = JsonConvert.SerializeObject(mediaEntry);
                    var mediaContent = new System.Net.Http.StringContent(json, System.Text.Encoding.UTF8);
                    mediaContent.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var response = await client.PostAsync("http://localhost:9000/media/add", mediaContent);

                    var responseString = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Addmedia Response Status Code: " + response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode.ToString() == "OK")
                        {

                            Debug.WriteLine("Media Successfully Added, MediaEntryId: " + mediaEntry.id);
                        }
                        else
                        {
                            Debug.WriteLine("Media Failed to add, MediaEntryId: " + mediaEntry.id);
                            Debug.WriteLine("Message String : " + response.ToString());
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Media Failed to add, MediaEntryId: " + mediaEntry.id);
                        Debug.WriteLine("Error String : " + response.ToString());
                    }

                    //string returnResponse = null;
                    //using (HttpContent content = response.Content)
                    //{
                    //    // ... Read the string.
                    //    Task<string> result = content.ReadAsStringAsync();
                    //    returnResponse = result.Result;
                    //    Debug.WriteLine("Addmedia: " + returnResponse);
                    //}


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
                log.Debug(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
            }
        }

        public class MediaEntry
        {
            public string id { get; set; }
            public string ownerId { get; set; }
            public bool isPrimary { get; set; }
            public string qivsPhotoId { get; set; }
            public MediaItem mediaItem { get; set; }

        }

        public class MediaItem
        {
            public string id { get; set; }
            public string fileName { get; set; }
            public string contentType { get; set; }
            public string status { get; set; }
            public string captureDate { get; set; }
            public string uploadedDate { get; set; }
            public bool isIncludedInInsurance { get; set; }
            public bool isIncludedInInsuranceReports { get; set; }
        }

        private void btn_processMediaMigration_Click(object sender, EventArgs e)
        {
            try
            {
                if (radio_btn_full_run.Checked)
                {
                    if (MessageBox.Show("Are You Sure?", "Are You Sure", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        Debug.WriteLine("Running");
                    }
                }
                else if (radio_btn_test_run.Checked)
                {
                    processMedia();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
                log.Debug(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
            }
        }

        private void processMedia()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
                log.Debug(MethodBase.GetCurrentMethod().Name + " Exception : " + ex.Message);
            }

        }
    }
}
