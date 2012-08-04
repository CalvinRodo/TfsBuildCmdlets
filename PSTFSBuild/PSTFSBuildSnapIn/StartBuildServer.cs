using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Build.Common;
using Microsoft.TeamFoundation.Client;

namespace AutomatedTFSbuild
{
    [Cmdlet(VerbsLifecycle.Start,"Build")]
    public class StartBuildServer : Cmdlet
    {
        [Alias("pname")]
        [Parameter(Mandatory = true)]
        public string ProjectName { get; set; }

        [ValidatePattern("http://(\\w)+:([0-9])+")]
        [Alias("server")]
        [Parameter(Mandatory = true)]
        public string TFSServer { get; set; }
        
        [Alias("dname")]
        [Parameter(Mandatory = true)]
        public string DefinitionName { get; set; }


        protected override void ProcessRecord()
        {

            TfsTeamProjectCollection tfstpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(TFSServer));            
            IBuildServer buildServer = tfstpc.GetService<IBuildServer>();
            IBuildDefinition buildDefinition = buildServer.GetBuildDefinition(ProjectName,DefinitionName);
            IBuildRequest buildRequest = buildDefinition.CreateBuildRequest();            
            buildRequest.GetOption = GetOption.LatestOnBuild;

            IQueuedBuild build = buildServer.QueueBuild(buildRequest);
            WriteObject(build);                     
        }             
        
    }
}
