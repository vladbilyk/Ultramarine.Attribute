using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace Ultramarine.Attribute.Powershell
{
    [CmdletProvider("PhotoMetadata", ProviderCapabilities.None)]
    public class PhotoMetadataProvider : ContainerCmdletProvider
    {
        private const string PathSeparator = "\\";

        private readonly string[] _treeItemNames = { "System", "System.GPS", "System.Photo", "System.Image" };

        protected override bool ItemExists(string path)
        {
            var itemName = GetItemName(path);
            if (string.IsNullOrEmpty(itemName))
            {
                return true;
            }
            if (IsTreeItemName(itemName))
            {
                return true;
            }
            if (IsLeafItemName(itemName))
            {
                return true;
            }
            return false;
        }

        private PhotoMetadata PhotoMetadata
        {
            get { return ((PhotoMetadataDriveInfo) PSDriveInfo).PhotoMetadata; }
        }

        private bool IsLeafItemName(string itemName)
        {
            return PhotoMetadata.Metadata.ContainsKey(itemName);
        }

        private bool IsTreeItemName(string itemName)
        {
            return _treeItemNames.Contains(itemName);
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
            var itemName = GetItemName(path);
            return !string.IsNullOrEmpty(itemName) && IsTreeItemName(itemName);
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            // TODO: use recurse param
            var itemName = GetItemName(path);
            if (string.IsNullOrEmpty(itemName))
            {
                WriteItemObject("System", path, true);
                return;
            }

            List<string> childItemNames = null;
            if (itemName == "System")
            {
                childItemNames = PhotoMetadata.Metadata.Keys.Where(key => key.StartsWith("System")
                                                                          && !key.StartsWith("System.GPS")
                                                                          && !key.StartsWith("System.Image")
                                                                          && !key.StartsWith("System.Photo")).ToList();
                foreach (var childItemName in childItemNames)
                {
                    WriteItemObject(PhotoMetadata.Metadata[childItemName], path, false);
                }
                WriteItemObject("System.GPS", path, true);
                WriteItemObject("System.Image", path, true);
                WriteItemObject("System.Photo", path, true);
            }
            else
            {
                childItemNames = PhotoMetadata.Metadata.Keys.Where(key => key.StartsWith(itemName)).ToList();
                foreach (var childItemName in childItemNames)
                {
                    WriteItemObject(PhotoMetadata.Metadata[childItemName], path, false);
                }
            }
        }

        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            var itemName = GetItemName(path);
            if (string.IsNullOrEmpty(itemName))
            {
                WriteItemObject("System", path, true);
                return;
            }

            List<string> childItemNames = null;
            if (itemName == "System")
            {
                childItemNames = PhotoMetadata.Metadata.Keys.Where(key => key.StartsWith("System")
                                                                          && !key.StartsWith("System.GPS")
                                                                          && !key.StartsWith("System.Image")
                                                                          && !key.StartsWith("System.Photo")).ToList();
                foreach (var childItemName in childItemNames)
                {
                    WriteItemObject(childItemName, path, false);
                }
                WriteItemObject("System.GPS", path, true);
                WriteItemObject("System.Image", path, true);
                WriteItemObject("System.Photo", path, true);
            }
            else
            {
                childItemNames = PhotoMetadata.Metadata.Keys.Where(key => key.StartsWith(itemName)).ToList();
                foreach (var childItemName in childItemNames)
                {
                    WriteItemObject(childItemName, path, false);
                }
            }
        }

        protected override void GetItem(string path)
        {
            var field = GetItemName(path);

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

        private string GetItemName(string path)
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
