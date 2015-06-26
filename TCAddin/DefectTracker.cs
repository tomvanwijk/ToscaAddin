using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.TCAddOns;

namespace DefectTracking
{
    public class DefectTracker : TCAddOn
    {

        public override string UniqueName
        {
            get 
            { 
                return "Defect Tracker v0.9"; 
            }
        }

        public override string DisplayedName
        {
            get
            {
                return "Defect Tracker";
            }
        }
    }
}
