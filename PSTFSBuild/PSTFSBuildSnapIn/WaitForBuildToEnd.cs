using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Microsoft.TeamFoundation.Build.Client;
using System.Threading;

namespace AutomatedTFSbuild
{
    [Cmdlet(VerbsLifecycle.Wait,"Build")]
    public class WaitForBuildToEnd : Cmdlet
    {
        [Parameter(ValueFromPipeline = true, Mandatory = true)]
        public IQueuedBuild build { get; set; }


        protected override void ProcessRecord()
        {
            ProgressRecord prog = new ProgressRecord(1,
                                                     "Waiting on Automated Build for " + build.TeamProject + " Definition: " + build.BuildDefinition.Name ,
                                                     "Place in Queue: " + build.QueuePosition);            
            //Unknown Progress
            prog.PercentComplete = -1;
            prog.SecondsRemaining = -1;
            prog.RecordType = ProgressRecordType.Processing;
            
            IBuildServer buildServer = build.BuildServer;
            IQueuedBuild updatedBuild = build;
            while (updatedBuild.Status == QueueStatus.Queued)
            {
                Thread.Sleep(1000);
                updatedBuild = buildServer.GetQueuedBuild(build.Id, QueryOptions.All);
                prog.StatusDescription = "Place in Queue: " + updatedBuild.QueuePosition;
                WriteProgress(prog);                                
            }
            
            prog.StatusDescription = "Building";
            while (updatedBuild.Status != QueueStatus.Completed)
            {

                WriteProgress(prog);                              

                //Sleep for a second.
                Thread.Sleep(1000);
                if (updatedBuild.Status == QueueStatus.Canceled)
                {
                    throw new Exception("Build has been Canceled");
                }
                updatedBuild = buildServer.GetQueuedBuild(build.Id, QueryOptions.All);
            }
            prog.RecordType = ProgressRecordType.Completed;
            WriteProgress(prog);
        }
    }
}
