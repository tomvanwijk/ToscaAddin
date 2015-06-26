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
        private TfsTeamProjectCollection tfs { get; set; }
        private string BKRpriority { get; set; }
        private string BKRfindingType { get; set; }

        public TFSConnector()
        {
            string TFSuri = DefectTrackerSettings.Default.TFSuri;
            string TFScollection = DefectTrackerSettings.Default.TFScollection;
            string TFSserver = TFSuri + TFScollection;
            BKRpriority = DefectTrackerSettings.Default.BKR_Priority;
            BKRfindingType = DefectTrackerSettings.Default.BKR_FindingType;
            Uri tfsUri = new Uri(TFSserver);
            tfs = new TfsTeamProjectCollection(tfsUri);
            workItemStore = (WorkItemStore)tfs.GetService(typeof(WorkItemStore));
        }

        public void CreateWorkitem()
        {
            WorkItemTypeCollection workItemTypes = workItemStore.Projects[DefectTrackerSettings.Default.TFSproject].WorkItemTypes;
            WorkItemType wiType = workItemTypes[DefectTrackerSettings.Default.TFSworkItemType];
            wi = new WorkItem(wiType);
        }

        public Uri GetWorkItemUrl(int workItemId)
        {
            TswaClientHyperlinkService service = tfs.GetService<TswaClientHyperlinkService>();
            if (service != null)
            {

                return service.GetWorkItemEditorUrl(workItemId);
            }
            return null;
        }

        public DefectResult CreateBug(Defect defect)
        {
            CreateWorkitem();
            DefectResult dr = new DefectResult();
            // set Data from defect to Work item
            wi.Fields["Title"].Value = defect.title;          
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
            // BKR-specific fields
            if (wi.Fields.Contains("BKR Priority") && wi.Fields.Contains("BKR Finding Type"))
            {
                wi.Fields["BKR Priority"].Value = BKRpriority;
                wi.Fields["BKR Finding Type"].Value = BKRfindingType;
            }

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

            // validate work item; check is all mandatory fields are used and if all fields have correct values
            ArrayList validationErrors = wi.Validate();

            if (validationErrors.Count == 0)
            {
                wi.Save();
                dr.id = wi.Id;
                dr.status = wi.State;
            }
            else
            {
                // show errors
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

        public List<Defect> GetTFSDefectsWithToscaDefects()
        {
            List<Defect> TFSDefects = new List<Defect>();
            Query q = new Query(workItemStore, "SELECT [How Found] FROM WorkItems WHERE [How Found] Contains '/Exec' AND [How Found] Contains  ';-' AND [State] <> 'Closed' ");
            WorkItemCollection wic = q.RunQuery();
            if (wic.Count != 0)
            {
                foreach (WorkItem wi in wic)
                {
                    string[] wiHowFoundArray = wi.Fields["How Found"].Value.ToString().Split(';');
                    if (wiHowFoundArray.Count() != 2)
                        continue;
                    TFSDefects.Add(new Defect() { uniqueid = wiHowFoundArray[1], id = wi.Id, status = wi.State});
                }
            }
            return TFSDefects;
        }


        public void DisposeTFS()
        {
            tfs.Dispose();
        }
    }
}
