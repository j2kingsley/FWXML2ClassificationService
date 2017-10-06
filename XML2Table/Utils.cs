using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using log4net;

namespace XML2Table
{
    public class Utils
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static int SafeInt(string inputString)
        {
            try
            {
                var retValue = 0;

                if (Int32.TryParse(inputString, out retValue))
                {
                    retValue = Int32.Parse(inputString);
                }

                return retValue;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                log.Error(MethodBase.GetCurrentMethod().Name + " | Exception : ", ex);
                return 0;
            }
        }

        public static DateTime? SafeDate(string inputString)
        {
            try
            {
                DateTime? retValue = null;
                DateTime outValue;

                if (DateTime.TryParse(inputString, out outValue))
                {
                    return outValue;
                }

                return retValue;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                log.Error(MethodBase.GetCurrentMethod().Name + " | Exception : ", ex);
                return null;
            }
        }

        public static string ValidateFolderPath([Optional] string stagingFolderPath, string folderName)
        {
            try
            {
                string folderPath = null;

                if (stagingFolderPath == null)
                {
                    stagingFolderPath = AppDomain.CurrentDomain.BaseDirectory + "Staging";
                    Directory.CreateDirectory(stagingFolderPath + @"\" + folderName);
                    folderPath = stagingFolderPath + @"\" + folderName;
                }
                else
                {
                    Directory.CreateDirectory(stagingFolderPath + @"\" + folderName);
                    folderPath = stagingFolderPath + @"\" + folderName;
                }

                return folderPath;
            }
            catch (Exception ex)
            {
                log.Error(MethodBase.GetCurrentMethod().Name + " | Exception : ", ex);
                return null;
            }

        }

       

        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
                log.Error("Warning: Matching schema not found.  No validation occurred." + e.Message);
            else // Error
                log.Error("Validation error: " + e.Message);
        }

       
        //public static DataTable Object2Table(IEnumerable<TransportServiceLicence> data)
        //{
        //    try
        //    {
        //        DataTable returnTable = null;
        //        using (var reader = ObjectReader.Create(data))
        //        {
        //            returnTable = new DataTable();

        //            returnTable.Load(reader);
        //        }

        //        return returnTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(MethodBase.GetCurrentMethod().Name + " | Exception : ", ex);
        //        return null;
        //    }
        //}
    }
}
