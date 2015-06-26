using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Text.RegularExpressions;


namespace DefectTracking
{
    public class WildCardID : TCAddOnTask
    {
        List<ModuleAttribute> moduleAttribs = new List<ModuleAttribute>();

        public override Type ApplicableType
        {
            get { return typeof(Module); }
        }

        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {
            Module mod = objectToExecuteOn as Module;
            moduleAttribs = mod.CurrentObjectMap.CurrentToModule.ModuleAttributes.ToList<ModuleAttribute>();

            foreach (ModuleAttribute ma in moduleAttribs)
            {
                Debug.WriteLine(ma.Name);
                Debug.WriteLine(ma.UniqueId);

                ObjectControlSimple ocs = ma.CurrentObjectControl as ObjectControlSimple;
                ocs.TechnicalId = Helper.RemoveWildCard(ocs.TechnicalId);
            }
            return null;
        }

        public override string Name
        {
            get { return "Update .NET random IDs"; }
        }

        public override bool IsTaskPossible(TCObject obj)
        {
            bool enabled = true;
            return enabled;
        }  
    }
}