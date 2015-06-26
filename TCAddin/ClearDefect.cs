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
            ee.ActualLog.Comment = String.Empty;
            ee.ActualLog.ChangeRequestId = String.Empty;
            return null;
        }

        public override string Name
        {
            get { return "Clear Defect"; }
        }

        public override bool IsTaskPossible(TCObject obj)
        {
            //BUITEN GEBRUIK
            return false;
            /*
            bool enabled = false;
            ExecutionEntry ee = obj as ExecutionEntry;
            // als er een executionentry is, dan is de optie toegestaan
            if (ee == null)
            {
                return false;
            }

            // als het resultaat Failed is, dan is de optie toegestaan.
            if (ee.ActualResult.ToString() == "Failed")
            {
                enabled = true;

                // als het changeRequestId NIET leeg is, dan is de optie toegestaan
                if (String.IsNullOrEmpty(ee.ActualLog.ChangeRequestId) & String.IsNullOrEmpty(ee.ActualLog.Comment))
                {
                    enabled = false;
                }
            }

            return enabled;
             */
        }
    }
}
