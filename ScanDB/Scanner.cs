using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.TCAddOns;

namespace ScanDB
{
    public class DBScanner: TCAddOn
    {
        public override string UniqueName
        {
            get { return "Database Scanner v0.1"; }
        }

        public override string DisplayedName
        {
            get { return "Database Scanner"; }
        }
    }
}
