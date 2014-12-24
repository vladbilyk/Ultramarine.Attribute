using System.IO;
using System.Management.Automation;

namespace Ultramarine.Attribute.Powershell
{
    [Cmdlet(VerbsCommon.Get, "Attribute")]
    public class GetAttributeCommand : PSCmdlet
    {
        [Parameter(
             //Position = 0,
             //ParameterSetName = "PatternParameterSet",
             //ValueFromPipeline = true,
             Mandatory = true)]
        //[Alias("PSPath")]
        public string Path { get; set; }

        protected override void ProcessRecord()
        {
            //var filePath = SessionState.Path.GetResolvedPSPathFromPSPath(Path);
            if (!File.Exists(Path))
            {
                ThrowTerminatingError(new ErrorRecord(new PSArgumentException("File is missing: " + Path, "Path"), 
                                                        "", ErrorCategory.InvalidArgument, Path));
            }
            WriteObject(Path);
        }
    }
}
