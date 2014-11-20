using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace Ultramarine.Attribute.Powershell
{
    [CmdletProvider("PhotoMetadata", ProviderCapabilities.None)]
    public class PhotoMetadataProvider : ContainerCmdletProvider
    {
        private const string PathSeparator = "\\";

        protected override bool ItemExists(string path)
        {
            //((PhotoMetadataDriveInfo)PSDriveInfo).PhotoMetadata.CheckPropertyName(path)
            return true; 
        }

        protected override bool IsValidPath(string path)
        {
            WriteVerbose("IsValidPath :" + path);
            throw new NotImplementedException();
        }

        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            if (drive == null)
            {
                WriteError(new ErrorRecord( new ArgumentNullException("drive"),
                                            "NullDrive",
                                            ErrorCategory.InvalidArgument,
                                            null));
                return null;
            }

            if (String.IsNullOrEmpty(drive.Root) || !File.Exists(drive.Root))
            {
                WriteError(new ErrorRecord( new ArgumentException("drive.Root"),
                                            "NoRoot",
                                            ErrorCategory.InvalidArgument,
                                            drive));
                return null;
            }

            return new PhotoMetadataDriveInfo(drive);
        }

        protected override bool HasChildItems(string path)
        {
            WriteVerbose("HasChildItems: " + path);
            var field = GetField(path);
            if (string.IsNullOrEmpty(field))
            {
                return true;
            }

            return ((PhotoMetadataDriveInfo) PSDriveInfo).PhotoMetadata.CheckSubProperties(field);
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            WriteVerbose("GetChildItems: " + path);
            //base.GetChildItems(path, recurse);
        }

        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            WriteVerbose("GetChildNames: " + path);
            var field = GetField(path);
            var list = ((PhotoMetadataDriveInfo)PSDriveInfo).PhotoMetadata.GetSubProperties(field);

            foreach (string s in list)
            {
                WriteItemObject(s, path, true);
            }
        }

        protected override void GetItem(string path)
        {
            var field = GetField(path);

            if (string.IsNullOrEmpty(field))
            {
                WriteItemObject(PSDriveInfo, path, true);
                return;
            }

            WriteItemObject(((PhotoMetadataDriveInfo)PSDriveInfo).PhotoMetadata.Metadata[field], path, false);
        }

        private static string NormalizePath(string path)
        {
            return string.IsNullOrEmpty(path) ? path : path.Replace("/", PathSeparator);
        }

        private string GetField(string path)
        {
            return NormalizePath(path).Replace(PSDriveInfo.Root + PathSeparator, string.Empty);
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
