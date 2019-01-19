using System.IO;

namespace Reevo.Unbroken.Extensions
{
    public static class FileSystemInfoExtension
    {
        /// <summary>
        ///     Creates all the directories required for this FileSystemInfo
        /// </summary>
        /// <param name="fileSystemInfo">The info about a file or folder</param>
        public static void CreateDirStructure(this FileSystemInfo fileSystemInfo)
        {
            var directoryInfo = fileSystemInfo as DirectoryInfo;
            var fileInfo = fileSystemInfo as FileInfo;

            if (directoryInfo != null)
            {
                if (!directoryInfo.Exists)
                {
                    CreateDirRecursive(directoryInfo);
                }
            }
            if (fileInfo != null)
            {
                if (!fileInfo.Exists)
                {
                    CreateDirRecursive(fileInfo.Directory);
                }
            }
        }

        /// <summary>
        /// Deletes a file or folder and makes sure recursive will be used if required
        /// </summary>
        /// <param name="fileSystemInfo">The file or folder to delete</param>
        /// <param name="recursiveIfRequired">if set to true and a folder is selected it will be deleted with all content, for a file this has no use</param>
        public static void Delete(this FileSystemInfo fileSystemInfo, bool recursiveIfRequired)
        {
            var directoryInfo = fileSystemInfo as DirectoryInfo;
            var fileInfo = fileSystemInfo as FileInfo;

            if (directoryInfo != null)
            {
                directoryInfo.Delete(recursiveIfRequired);
            }
            if (fileInfo != null)
            {
                fileInfo.Delete();
            }
        }

        private static void CreateDirRecursive(DirectoryInfo directoryInfo)
        {
            if (directoryInfo.Parent != null && !directoryInfo.Parent.Exists)
            {
                CreateDirRecursive(directoryInfo.Parent);
            }
            directoryInfo.Create();
        }
    }
}