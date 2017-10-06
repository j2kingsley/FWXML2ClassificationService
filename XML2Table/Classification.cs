using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML2Table
{
    public class Classification
    {
        public string category { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string shortDescription { get; set; }
        public bool isActive { get; set; }
        public string parent_category { get; set; }
        public string parent_code { get; set; }
    }
}
