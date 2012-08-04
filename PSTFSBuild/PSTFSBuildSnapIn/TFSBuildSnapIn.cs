using System.Management.Automation;
using System.ComponentModel;
using System.Configuration.Install;

namespace AutomatedTFSbuild
{
    [RunInstaller(true)]
    public class TFSBuildSnapIn : PSSnapIn
    {
        /// <summary>
        /// Create an instance of the GetProcPSSnapIn01 class.
        /// </summary>
        public TFSBuildSnapIn()
            : base()
        {
        }

        /// <summary>
        /// Specify the name of the PowerShell snap-in.
        /// </summary>
        public override string Name
        {
            get
            {
                return "PSTFSBuild";
            }
        }

        /// <summary>
        /// Specify the vendor for the PowerShell snap-in.
        /// </summary>
        public override string Vendor
        {
            get
            {
                return "Calvin Rodo";
            }
        }


        /// <summary>
        /// Specify a description of the PowerShell snap-in.
        /// </summary>
        public override string Description
        {
            get
            {
                return "CmdLets that allow you to work with a TFS Build Server.";
            }
        }
    }

}