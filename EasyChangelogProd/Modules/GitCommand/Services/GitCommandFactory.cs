using EasyChangelogProd.Modules.GitCommand.Interfaces;
using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;

namespace EasyChangelogProd.Modules.GitCommand.Services;

public class GitCommandFactory(IGitCommandAppSettings gitCommandAppSettings) : IMarkdownChangelogGitCommand
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