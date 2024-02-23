using EasyChangelogProd.Modules.GitCommand.Interfaces;

namespace EasyChangelogProd.Modules.GitCommand.Services;

public class GitCommandFactory(IGitCommandAppSettings gitCommandAppSettings) : IGitCommandFactory
{
    IGitCommandProduct IGitCommandFactory.BuildGitCommandOutputQ1Product()
    {
        var results = (IGitCommandProduct)new GitCommandOutputQ1Product(gitCommandAppSettings);
        results.SetChangelogSettingsPrune()
            .SetGitExePath()
            .SetGitRepoPath()
            .SetProcessStartInfoArgs()
            .Build();

        return results;
    }
}