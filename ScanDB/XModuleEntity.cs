using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanDB
{
    public class XModuleEntity
    {
        public String Name { get; set; }
        public String ConnectionString { get; set; }
        public String OrderAttribute { get; set; }
       
        public List<String> Attributes { get; set; }

    }
}
