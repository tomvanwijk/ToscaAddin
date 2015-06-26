using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Collections;
using System.IO;

namespace DefectTracking
{
    public class TFSConnector
    {
        private WorkItem wi { get; set; }
        private WorkItemStore workItemStore { get; set; }
        TfsTeamProjectCollection tfs { get; set; }
        private string BKRpriority { get; set; }
        private string BKRfindingType { get; set; }

        public TFSConnector()
        {
            string TFSuri = DefectTrackerSettings.Default.TFSuri;
            string TFScollection = DefectTrackerSettings.Default.TFScollection;
            string TFSserver = TFSuri + TFScollection;
            string TFSproject = DefectTrackerSettings.Default.TFSproject;
            string TFSworkItemType = DefectTrackerSettings.Default.TFSworkItemType;
            BKRpriority = DefectTrackerSettings.Default.BKR_Priority;
            BKRfindingType = DefectTrackerSettings.Default.BKR_FindingType;
            Uri tfsUri = new Uri(TFSserver);
            tfs = new TfsTeamProjectCollection(tfsUri);
            workItemStore = (WorkItemStore)tfs.GetService(typeof(WorkItemStore));
            WorkItemTypeCollection workItemTypes = workItemStore.Projects[TFSproject].WorkItemTypes;
            WorkItemType wiType = workItemTypes[TFSworkItemType];
            wi = new WorkItem(wiType);
        }

        public DefectResult CreateBug(Defect defect)
        {
            DefectResult dr = new DefectResult();
            // gegevens uit Defect object in een work item zetten.
            wi.Fields["Title"].Value = defect.title;
            //wi.Fields["Description"].Value = defect.description;
            wi.Fields["How Found"].Value = defect.howFound;
            wi.Fields["Symptom"].Value = "<PRE>"+ defect.symptom + "</PRE>";

            if (defect.attachmentList != null)
            {
                foreach (string attachment in defect.attachmentList)
                {
                    if (File.Exists(attachment))
                    {
                        wi.Attachments.Add(new Attachment(attachment, "Added by Tosca Defect Tracker"));
                    }
                }
            }
            // static fields
            wi.Fields["BKR Priority"].Value = BKRpriority;
            wi.Fields["BKR Finding Type"].Value = BKRfindingType;

            #region Not used
            //wi.Fields["History"].Value = "";
            //wi.Fields["Iteration Path"].Value = "";
            //wi.Fields["Iteration ID"].Value = "";
            //wi.Fields["Activated Date"].Value = "";
            //wi.Fields["Activated By"].Value = "";
            //wi.Fields["Resolved Date"].Value = "";
            //wi.Fields["Resolved By"].Value = "";
            //wi.Fields["Resolved Reason"].Value = "";
            //wi.Fields["Stack Rank"].Value = "";
            //wi.Fields["Blocked"].Value = "";
            //wi.Fields["State"].Value = "";
            //wi.Fields["Changed By"].Value = "";
            //wi.Fields["Reason"].Value = "";
            //wi.Fields["Assigned To"].Value = "";
            //wi.Fields["BKR Clarify Date"].Value = "";
            //wi.Fields["Root Cause"].Value = "";
            //wi.Fields["Severity"].Value = "";
            //wi.Fields["System Info"].Value = "";
            //wi.Fields["Repro Steps"].Value = "";
            //wi.Fields["Found In Environment"].Value = "";
            //wi.Fields["Proposed Fix"].Value = "";
            //wi.Fields["BKR Clarify By"].Value = "";
            //wi.Fields["Area Path"].Value = "";
            //wi.Fields["Area ID"].Value = "";
            #endregion

            // work item valideren; controleren of alle verplichte velden gevuld zijn en of alle waarden correct zijn ingevuld
            ArrayList validationErrors = wi.Validate();

            // als er geen fouten zijn, work item opslaan en bugid ophalen
            if (validationErrors.Count == 0)
            {
                wi.Save();
                dr.id = wi.Id;
                dr.status = wi.State;
            }
            else
            {
                // foutmeldingen tonen
                foreach (Field field in validationErrors)
                {
                    System.Windows.Forms.MessageBox.Show("Validation Error in field " + field.ReferenceName);
                }
                dr.id = 0;
                dr.status = String.Empty;
            }
            return dr;
        }

        public DefectResult GetDefect(string sId)
        {
            DefectResult dr = new DefectResult();
            int id = Convert.ToInt32(sId);

            wi = GetWorkItem(id);
            if (wi != null)
            {
                dr.id = wi.Id;
                dr.status = wi.State;
            }
            return dr;
        }

        private WorkItem GetWorkItem(int id)
        {
            Query q = new Query(workItemStore, "SELECT System.ID FROM WorkITems WHERE [System.ID] = '"+id+"'");
            WorkItemCollection wic = q.RunQuery();
            if (wic.Count != 0)
            {
                return wic[0];
            }
            else
            {
                return null;
            }
        }

        public void DisposeTFS()
        {
            tfs.Dispose();
        }
    }
}
