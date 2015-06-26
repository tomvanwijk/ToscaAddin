using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace DefectTracking
{
    public class OpenDefect : TCAddOnTask
    {
        public override Type ApplicableType
        {
            get { return typeof(ExecutionListItem); }
        }

        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {
            ExecutionEntry ee = objectToExecuteOn as ExecutionEntry;
            int bugid = 0;
            Int32.TryParse(ee.ActualLog.ChangeRequestId, out bugid);
            if (bugid > 0)
            {
                TFSConnector tfsc = new TFSConnector();
                Process.Start(tfsc.GetWorkItemUrl(bugid).ToString());
            }
            return null;
        }

        public override string Name
        {
            get { return "Open Defect"; }
        }

        public override bool IsTaskPossible(TCObject obj)
        {
            bool enabled = false;
            ExecutionEntry ee = obj as ExecutionEntry;
            if (ee == null || ee.ActualLog == null)
            { 
                return false; 
            }

            if (!String.IsNullOrEmpty(ee.ActualLog.ChangeRequestId))
            {
                enabled = true;
            }
            
            return enabled;
        }       
    }
}
