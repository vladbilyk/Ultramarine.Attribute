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

        protected override bool IsValidPath(string path)
        {
            WriteVerbose(string.Format("Path: {0} is validated", path));
            // TODO: implement
            return true;
        }

        protected override bool ItemExists(string path)
        {
            return ((PhotoMetadataDriveInfo)PSDriveInfo).PhotoMetadata.CheckPropertyName(path); 
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

        protected override void GetItem(string path)
        {
            //TODO: check this implementation
            // Check to see if the path represents a valid drive.
            if (PathIsDrive(path))
            {
                WriteItemObject(PSDriveInfo, path, true);
                return;
            }

            var request = NormalizePath(path).Replace(PSDriveInfo.Root + PathSeparator, string.Empty);

            var obj = ((PhotoMetadataDriveInfo)PSDriveInfo).PhotoMetadata.Metadata[request];

            WriteItemObject(obj, path, false);
        }

        /// <summary>
        /// Checks to see if a given path is actually a drive name.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>
        /// True if the path given represents a drive, otherwise false 
        /// is returned.
        /// </returns>
        private bool PathIsDrive(string path)
        {
            // Remove the drive name and first path separator.  If the 
            // path is reduced to nothing, it is a drive. Also, if it is
            // just a drive then there will not be any path separators.
            return String.IsNullOrEmpty(
                path.Replace(PSDriveInfo.Root, string.Empty)) ||
                   String.IsNullOrEmpty(
                       path.Replace(PSDriveInfo.Root + PathSeparator, string.Empty));
        }

        private string NormalizePath(string path)
        {
            var result = path;

            if (!String.IsNullOrEmpty(path))
            {
                result = path.Replace("/", PathSeparator);
            }

            return result;
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
