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
            var psPaths = SessionState.Path.GetResolvedPSPathFromPSPath(Path);
            foreach (var p in psPaths)
            {
                WriteObject(new PhotoMetadata(p.Path).Metadata);
            }
        }
    }
}
