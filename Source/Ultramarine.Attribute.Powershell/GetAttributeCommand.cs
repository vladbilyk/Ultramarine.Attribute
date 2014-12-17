using System.Management.Automation;

namespace Ultramarine.Attribute.Powershell
{
    [Cmdlet(VerbsCommon.Get, "Attribute")]
    public class GetAttributeCommand : Cmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject("Hello from cmdlet!");
        }
    }
}
