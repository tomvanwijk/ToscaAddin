using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace DefectTracking
{
    class UpdateDefect : TCAddOnTask
    {
        public override Type ApplicableType
        {
            get { return typeof(ExecutionListItem); }
        }

        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {
            ExecutionEntry ee = objectToExecuteOn as ExecutionEntry;
            TFSConnector tfs = new TFSConnector();
            DefectResult dr = tfs.GetDefect(ee.ActualLog.ChangeRequestId);
            tfs.DisposeTFS();
            if (!String.IsNullOrEmpty(dr.status))
            {
                ee.ActualLog.Comment = dr.status;
            }
            return null;
        }

        public override string Name
        {
            get { return "Update Defect"; }
        }

        public override bool IsTaskPossible(TCObject obj)
        {
            bool enabled = false;
            ExecutionEntry ee = obj as ExecutionEntry;
            // als er een executionentry is, dan is de optie toegestaan
            if (ee == null || ee.ActualLog == null)
            {
                return false;
            }

            // als het resultaat Failed is, dan is de optie toegestaan.
            if (ee.ActualResult.ToString() == "Failed")
            {
                enabled = true;

                // als het changeRequestId NIET leeg is, dan is de optie toegestaan
                if (!String.IsNullOrEmpty(ee.ActualLog.ChangeRequestId))
                {
                    enabled = true;
                }
                else
                {
                    enabled = false;
                }
            }

            return enabled;
        }
    }
}
