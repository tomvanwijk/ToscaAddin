using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;


namespace DefectTracking
{
    public class SubmitDefect : TCAddOnTask
    {
        public override Type ApplicableType
        {
            get { return typeof(ExecutionListItem); }
        }
       
        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {
            //klasse aanmaken om het resultaat van het defect in op te slaan
            DefectResult dr = new DefectResult();
            List<String> uidList = new List<String>();
            List<String> files = new List<String>();
            IEnumerable<DirectoryInfo> directories;

            ExecutionEntry ee = objectToExecuteOn as ExecutionEntry;

            // plaatjes van foutieve testcases ophalen.

            //var dirinfo = new DirectoryInfo(@"C:\TOSCA_PROJECTS\ToscaCommander\Screenshots\failedTestSteps");
            var dirinfo = new DirectoryInfo(DefectTrackerSettings.Default.ToscaScreenshotFolder);
            if (dirinfo.Exists)
            {
                directories = dirinfo.GetDirectories().Where(d => d.LastWriteTime > DateTime.Now.AddDays(-1));
            }
            else
            {
                directories = null;
            }
            

            foreach (var ts in ee.TestCase.Items)
            {
                if (ts is TestStep)
                {
                    TestStep ts2 = ts as TestStep;
                    foreach (TestStepValue tsv in ts2.TestStepValues)
                    {
                        if (directories != null)
                        {
                            var result = directories.Where(b => b.Name == "surrogate " + tsv.UniqueId.ToString() && b.CreationTime > DateTime.Now.AddDays(-1)).Select(a => (tsv.UniqueId.ToString()));
                            foreach (string r in result)
                            {
                                uidList.Add("surrogate " + tsv.UniqueId.ToString());
                            }
                        }
                    }
                }
            }

            foreach (string dir in uidList)
            {
                files = Directory.GetFiles(DefectTrackerSettings.Default.ToscaScreenshotFolder + @"\" + dir).ToList();
            }


            SetTFSProject(ee);
            Defect defect;
            if (files.Count != 0)
            {
                defect = SetDefect(ee, files);
            }
            else 
            {
                defect = SetDefect(ee);
            }

            TFSConnector tfs = new TFSConnector();
            dr = tfs.CreateBug(defect);
            tfs.DisposeTFS();
            if (dr.id != 0) 
            {
                ee.ActualLog.ChangeRequestId = dr.id.ToString();
                ee.ActualLog.ChangeRequestState = dr.status;
            }
            //// start IE om de bevinding in TFS te bekijken
            Process.Start(DefectTrackerSettings.Default.TFSuri + DefectTrackerSettings.Default.TFSwebUri + dr.id.ToString());

            return null;
        }

        private static Defect SetDefect(ExecutionEntry ee)
        {
            Defect defect = new Defect();
            defect.title = ee.ActualLog.Name;
            defect.symptom = ee.ActualLog.AggregatedDescription;
            defect.howFound = ee.ActualLog.NodePath;
            return defect;
        }

        private static Defect SetDefect(ExecutionEntry ee, List<string> attachment)
        {
            Defect defect = new Defect();
            defect.title = ee.ActualLog.Name;
            defect.symptom = ee.ActualLog.AggregatedDescription;
            defect.howFound = ee.ActualLog.NodePath;
            defect.attachmentList = attachment;
            return defect;
        }

        private static void SetTFSProject(ExecutionEntry ee)
        {
            IObjectWithConfiguration iObj = ee;
            string[] configurationParameters = TestConfiguration.GetTestConfigurationParameterNames(iObj);
            int index = Array.IndexOf(configurationParameters, DefectTrackerSettings.Default.ToscaTFSProject);
            if (index != -1)
            {
                string configurationValue = TestConfiguration.GetTestConfigurationParameterValue(iObj, DefectTrackerSettings.Default.ToscaTFSProject);
                DefectTrackerSettings.Default.TFSproject = configurationValue;
                DefectTrackerSettings.Default.Save();
            }
        }

        public override string Name
        {
            get { return "Submit Defect"; }
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

                // als het changeRequestId leeg is, dan is de optie toegestaan
                if (String.IsNullOrEmpty(ee.ActualLog.ChangeRequestId))
                {
                    enabled = true;
                }
                else
                {
                    enabled = false;
                    // als changeRequestId niet leeg is en comment is gevuld met closed, dan is de optie toegestaan
                    if (ee.ActualLog.ChangeRequestState == "Closed")
                    {
                        enabled = true;
                    }

                }
            }
            
            return enabled;
        }       
    }
}
