using System;
using System.IO;

namespace Reevo.Unbroken.Extensions
{
    public static class FileInfoExtensions
    {
        public static bool FileExistsOnEnvironmentPaths(this FileInfo file)
        {
            return !string.IsNullOrEmpty(GetFileFromEnvironmentPaths(file));
        }

        public static string GetFileFromEnvironmentPaths(this FileInfo file)
        {
            var values = Environment.GetEnvironmentVariable("PATH");

            if (values == null)
            {
                return null;
            }

            foreach (var path in values.Split(Path.PathSeparator))
            {
                var fullPath = Path.Combine(path, file.Name);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }

            return null;
        }
    }
}
