using System.Reflection;

namespace EasyChangelogProd.Extensions;

public static class PathExtensions
{
    public static string GetExecutablePath(this Assembly assembly)
    {
        if (assembly == null) throw new ArgumentNullException(nameof(assembly));


        // Get the location of the assembly (which includes the executable path)
        var exePath = assembly.Location;

        // You might want to get the directory instead of the full path
        var exeDirectory = Path.GetDirectoryName(exePath);

        return exeDirectory ?? throw new Exception("There was an error in GetExecutablePath()");
    }

    public static string FindMatchingParentFolder(this string startPath, string targetFolderName)
    {
        var currentPath = startPath;

        while (!string.IsNullOrEmpty(currentPath))
        {
            var parentDirectoryInfo = Directory.GetParent(currentPath);

            if (parentDirectoryInfo == null) break; // Reached the root or an invalid path

            var parentPath = parentDirectoryInfo.FullName;

            // If a folder with the target name exists under this parent, return the parent's path
            if (Directory.Exists(Path.Combine(parentPath, targetFolderName)))
                return Path.Combine(parentPath, targetFolderName);

            currentPath = parentPath;
        }

        return null; // No matching parent folder found
    }

    public static bool IsFileModifiedRecently(this string filePath, TimeSpan threshold)
    {
        if (File.Exists(filePath))
        {
            var creationTime = File.GetLastWriteTime(filePath);
            var currentTime = DateTime.Now;

            return currentTime - creationTime <= threshold;
        }

        // If the file doesn't exist, you might consider how to handle this case
        return false;
    }
}