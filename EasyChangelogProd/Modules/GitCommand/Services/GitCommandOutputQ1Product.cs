using System.Diagnostics;
using System.Reflection;
using EasyChangelogProd.Extensions;
using EasyChangelogProd.Modules.AppSettings.Models;
using EasyChangelogProd.Modules.GitCommand.Interfaces;
using EasyChangelogProd.Modules.GitCommand.Models;

namespace EasyChangelogProd.Modules.GitCommand.Services;

public class GitCommandOutputQ1Product(IGitCommandAppSettings gitCommandAppSettings) : IGitCommandProduct
{
    private ChangelogSettings__prune? _changelogSettingsPrune;
    private string? _gitExePath;
    private string? _gitLogOutputText;
    private string? _gitRepoPath;
    private string? _processStartInfoArgs;

    IGitCommandProduct IGitCommandProduct.SetChangelogSettingsPrune()
    {
        _changelogSettingsPrune = gitCommandAppSettings.GetChangelogSettingsPrune();
        return this;
    }

    IGitCommandProduct IGitCommandProduct.SetGitExePath()
    {
        _gitExePath = _changelogSettingsPrune?.GitExePath ?? throw new Exception("GitExePath must not be null");
        return this;
    }

    IGitCommandProduct IGitCommandProduct.SetGitRepoPath()
    {
        var targetFolderName = gitCommandAppSettings.GetChangelogSettingsPrune().AppName;
        if (targetFolderName != null)
            _gitRepoPath = Assembly
                .GetExecutingAssembly()
                .GetExecutablePath()
                .FindMatchingParentFolder(targetFolderName);


        return this;
    }

    IGitCommandProduct IGitCommandProduct.SetProcessStartInfoArgs()
    {
        _processStartInfoArgs = _changelogSettingsPrune?.ProcessStartInfoArgs ??
                                throw new Exception("ProcessStartInfoArgs must not be null");

        return this;
    }

    IGitCommandProduct IGitCommandProduct.Build()
    {
        try
        {
            if (_gitExePath == null) throw new Exception("_gitExePath should not be null");

            if (_gitRepoPath == null) throw new Exception("_gitRepoPath should not be null");

            if (_processStartInfoArgs == null) throw new Exception("_processStartInfoArgs should not be null");


            var startInfo = new ProcessStartInfo
            {
                FileName = _gitExePath,
                Arguments = _processStartInfoArgs,
                WorkingDirectory = _gitRepoPath,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };


            using (var process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();

                // Capture the output of the command
                _gitLogOutputText = process.StandardOutput.ReadToEnd();

                process.WaitForExit(); // Wait for the process to exit (optional)

                // Now you can handle the gitOutput as needed
                //Console.WriteLine(gitOutput);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex, $"Unhandled exception! {ex.Message}");
        }

        return this;
    }

    List<GitCommandOutput__flat> IGitCommandProduct.GetGitCommandOutput()
    {
        var results = new List<GitCommandOutput__flat>();

        try
        {
            if (string.IsNullOrEmpty(_gitLogOutputText))
                throw new Exception("gitLogOutputText cannot be null or empty");

            //var rows = _gitLogOutputText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var rows = _gitLogOutputText.Split(new[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var r in rows)
            {
                var fields = r.Split(["|~|"], StringSplitOptions.TrimEntries);

                if (fields[3].Trim().Equals(string.Empty))
                    results.Add(new GitCommandOutput__flat
                    {
                        ConventionalCommitTag = fields[2].Contains(':') ? fields[2].Split(':')[0] : "none",
                        DateOfCommit = Convert.ToDateTime(fields[0]),
                        Version = fields[1],
                        FullMessage = fields[2],
                        MessageNoTag = fields[2].Contains(':') ? fields[2].Split(':')[1] : fields[2].Split(':')[0]
                    });
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex, $"Unhandled exception! {ex.Message}");
        }

        return results;
    }
}