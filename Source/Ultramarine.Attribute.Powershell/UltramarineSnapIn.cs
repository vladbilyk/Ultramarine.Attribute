using System.ComponentModel;
using System.Management.Automation;

namespace Ultramarine.Attribute.Powershell
{
    [RunInstaller(true)]
    public class UltramarineSnapIn : PSSnapIn
    {
        public override string Name
        {
            get { return "UltramarineSnapIn"; }
        }

        public override string Vendor
        {
            get { return "Vlad Bilyk"; }
        }

        public override string Description
        {
            get { return "This is a PowerShell snap-in that includes the Ultramarine Attribute providr"; }
        }
    }
}
