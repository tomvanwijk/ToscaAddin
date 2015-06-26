using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.TCAddOns;

namespace DefectTracking
{
    public class IDUpdater : TCAddOn
    {

        public override string UniqueName
        {
            get
            {
                return "ID Updater v0.9";
            }
        }

        public override string DisplayedName
        {
            get
            {
                return "ID Updater";
            }
        }
    }
}
