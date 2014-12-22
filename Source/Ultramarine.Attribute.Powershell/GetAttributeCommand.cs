using System.Management.Automation;

namespace Ultramarine.Attribute.Powershell
{
    [Cmdlet(VerbsCommon.Get, "Attribute")]
    public class GetAttributeCommand : Cmdlet
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
            WriteObject(Path);
        }
    }
}
