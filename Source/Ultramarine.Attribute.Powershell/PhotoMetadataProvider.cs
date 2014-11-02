using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace Ultramarine.Attribute.Powershell
{
    [CmdletProvider("PhotoMetadata", ProviderCapabilities.None)]
    public class PhotoMetadataProvider : ItemCmdletProvider
    {
        protected override bool IsValidPath(string path)
        {
            WriteVerbose(string.Format("Path: {0} is validated", path));

            return true;
        }

        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            if (drive == null)
            {
                WriteError(new ErrorRecord(
                           new ArgumentNullException("drive"),
                           "NullDrive",
                           ErrorCategory.InvalidArgument,
                           null));
                return null;
            }

            if (String.IsNullOrEmpty(drive.Root) || !File.Exists(drive.Root))
            {
                WriteError(new ErrorRecord(
                           new ArgumentException("drive.Root"),
                           "NoRoot",
                           ErrorCategory.InvalidArgument,
                           drive));
                return null;
            }

            return new PhotoMetadataDriveInfo(drive);
        }
    }

    public class PhotoMetadataDriveInfo : PSDriveInfo
    {
        public PhotoMetadataDriveInfo(PSDriveInfo drive) :
            base(drive)
        {
            PhotoMetadata = new PhotoMetadata(drive.Root);
        }

        internal PhotoMetadata PhotoMetadata { get; private set; }
    }
}
