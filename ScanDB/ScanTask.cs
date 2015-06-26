using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace ScanDB
{
    class ScanTask : TCAddOnTask
    {
        public override Type ApplicableType
        {
            get { return typeof(TCFolder); }
        }

        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {
            DBScanWindow form = new DBScanWindow();
            form.ShowDialog();
            // If the user has not selected anyting
            if (form.XModule == null)
                return null;
            
            XModuleEntity xme = form.XModule;

            TCFolder folder = objectToExecuteOn as TCFolder;
            XModule xmodule = folder.CreateXModule();
            xmodule.Name = xme.Name;
            
            XParam engine = xmodule.CreateConfigurationParam();
            engine.Name = "Engine";
            engine.Value = "Engine";

            XParam set = xmodule.CreateConfigurationParam();
            set.Name = "SpecialExecutionTask";
            set.Value = "CompareDB";

            XParam connString = xmodule.CreateConfigurationParam();
            connString.Name = "ConnectionString";
            connString.Value = xme.ConnectionString;
            
            XParam tableName = xmodule.CreateConfigurationParam();
            tableName.Name = "TableName";
            tableName.Value = xme.Name;

            XParam orderAttrib = xmodule.CreateConfigurationParam();
            orderAttrib.Name = "OrderAttribute";
            orderAttrib.Value = xme.OrderAttribute;
           

            foreach (string item in xme.Attributes)
            {
                XModuleAttribute xma = xmodule.CreateModuleAttribute();
                xma.Name = item;
                xma.DefaultActionMode = XTestStepActionMode.Verify;
                XParam p1 = xma.CreateConfigurationParam();
                p1.Name = "Parameter";
                p1.Value = "True";
                XParam p2 = xma.CreateConfigurationParam();
                p2.Name = "ExplicitName";
                p2.Value = "True";    
            }
            return null;            
        }

        public override string Name
        {
            get { return "Scan DB"; }
        }

        public override bool IsTaskPossible(TCObject obj)
        {
            if (obj is TCFolder && obj.NodePath.StartsWith("/Modules")) return true;
            return false;
        }

    }
}
