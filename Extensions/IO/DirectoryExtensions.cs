using System.IO;

namespace Reevo.Unbroken.Extensions
{
    public static class DirectoryExtensions
    {
        /// <summary>
        /// Recursively deletes a directory as well as any subdirectories and files. If the files are read-only, they are flagged as normal and then deleted.
        /// </summary>
        /// <param name="directory">The name of the directory to remove.</param>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void DeleteReadOnlyDirectory(string directory)
        {
            var dir = new DirectoryInfo(directory);
            DeleteReadOnlyDirectory(dir);
        }

        /// <summary>
        /// Recursively deletes a directory as well as any subdirectories and files. If the files are read-only, they are flagged as normal and then deleted.
        /// </summary>
        /// <param name="directory">The directory to remove.</param>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void DeleteReadOnlyDirectory(this DirectoryInfo directory)
        {
            foreach (var subdirectory in directory.EnumerateDirectories())
            {
                DeleteReadOnlyDirectory(subdirectory);
            }

            foreach (var fileInfo in directory.EnumerateFiles())
            {
                fileInfo.Attributes = FileAttributes.Normal;
                fileInfo.Delete();
            }

            directory.Delete();
        }
    }
}
