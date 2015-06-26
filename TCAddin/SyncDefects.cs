using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace DefectTracking
{
    class SyncDefects : TCAddOnTask
    {
        private List<ExecutionEntry> entries = new List<ExecutionEntry>();
        private List<ExecutionList> lists = new List<ExecutionList>();

        public override Type ApplicableType
        {
            get { return typeof(TCObject); }
        }

        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {
            if (objectToExecuteOn is TCFolder)
            {
                AddExecutionEntries(objectToExecuteOn as TCFolder);
            }

            if (objectToExecuteOn is ExecutionEntryFolder)
            {
                AddExecutionEntries(objectToExecuteOn as ExecutionEntryFolder);
            }

            if (objectToExecuteOn is ExecutionList)
            {
                AddExecutionEntries(objectToExecuteOn as ExecutionList);
            }
            TFSConnector tfs = new TFSConnector();
            foreach (ExecutionEntry ee in entries)
            {
                if (IsInteger(ee.ActualLog.ChangeRequestId))
                {
                    DefectResult dr = tfs.GetDefect(ee.ActualLog.ChangeRequestId);
                    ee.ActualLog.ChangeRequestState = dr.status;
                }
            }
            tfs.GetTFSDefectsWithToscaDefects();
            tfs.DisposeTFS();
            return null;
        }

        public override string Name
        {
            get { return "Sync Defects"; }
        }

        public override bool IsTaskPossible(TCObject obj)
        {
            bool enabled = false;
            if (obj is ExecutionEntryFolder || obj is ExecutionList)
            {
                enabled = true;
            }
            else if (obj is TCFolder)
            {
                if ((obj as TCFolder).PossibleContent.Contains("Execution"))
                {
                    enabled = true;
                }
            }
            return enabled;
        }

        private void AddExecutionEntries(TCFolder tf)
        {
            GetTCFolder(tf);
            foreach (ExecutionList el in lists)
            {
                AddExecutionEntries(el);
            }
        }

        private void AddExecutionEntries(ExecutionList el)
        {
            if (!el.ChangesAllowed)
            {
                return;
            }
            foreach (var elItem in el.Items)
            {
                AddExecutionEntries(elItem);

                if (elItem is ExecutionEntryFolder)
                {
                    AddExecutionEntries(elItem as ExecutionEntryFolder);
                }
            }
        }

        private void AddExecutionEntries(ExecutionEntryFolder eef)
        {
            foreach (var eefItem in eef.Items)
            {
                AddExecutionEntries(eefItem);
            }
        }

        private void AddExecutionEntries(ExecutionListItem eefItem)
        {
            if (eefItem is ExecutionEntry)
            {
                ExecutionEntry ee = eefItem as ExecutionEntry;
                if (ee != null && ee.ActualLog != null)
                {
                    if (!String.IsNullOrEmpty(ee.ActualLog.ChangeRequestId))
                    {
                        entries.Add(ee);
                    }
                }

            }
        }

        private void GetTCFolder(TCFolder tf)
        {
            foreach (OwnedItem oi in tf.Items)
            {
                if (oi is ExecutionList)
                {
                    lists.Add(oi as ExecutionList);
                }
                if (oi is TCFolder)
                {
                    GetTCFolder(oi as TCFolder);
                }
            }
        }

        public bool IsInteger(string d)
        {
            int i;
            Int32.TryParse(d, out i);
            if (i == 0) return false;
            else return true;
        }
    }
}
