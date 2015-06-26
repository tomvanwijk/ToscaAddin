using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace DefectTracking
{
    class ClearDefect : TCAddOnTask

    {
        public override Type ApplicableType
        {
            get { return typeof(ExecutionListItem); }
        }

        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {
            ExecutionEntry ee = objectToExecuteOn as ExecutionEntry;
            ee.ActualLog.ChangeRequestState = String.Empty;
            ee.ActualLog.ChangeRequestId = String.Empty;
            return null;
        }

        public override string Name
        {
            get { return "Clear Defect"; }
        }

        public override bool IsTaskPossible(TCObject obj)
        {
            //used for debugging
            return false;
            /*
            bool enabled = false;
            ExecutionEntry ee = obj as ExecutionEntry;
            if (ee == null)
            {
                return false;
            }

            if (ee.ActualResult.ToString() == "Failed")
            {
                enabled = true;

                if (String.IsNullOrEmpty(ee.ActualLog.ChangeRequestId) & String.IsNullOrEmpty(ee.ActualLog.ChangeRequestState))
                {
                    enabled = false;
                }
            }

            return enabled;
             */
        }
    }
}
