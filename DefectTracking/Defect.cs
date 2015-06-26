using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefectTracking
{
    public class Defect
    {
        public Defect() { }

        public int id { get; set; }
        public string uniqueid { get; set; }
        public string title { get; set; }
        public string howFound { get; set; }
        public string symptom { get; set; }
        public string status { get; set; }
        public List<string> attachmentList { get; set; }

    }

    public class DefectResult
    {
        public DefectResult() {}

        public int id {get; set;}
        public string status {get; set; }   
    }
}
