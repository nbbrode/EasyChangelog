using EasyChangelogProd.Modules.GitCommand.Models;

namespace EasyChangelogProd.Modules.GitCommand.Interfaces;

public interface IGitCommandProduct
{
    public IGitCommandProduct SetChangelogSettingsPrune();
    public IGitCommandProduct SetGitExePath();
    public IGitCommandProduct SetGitRepoPath();
    public IGitCommandProduct SetProcessStartInfoArgs();
    public IGitCommandProduct Build();
    public List<GitCommandOutput__flat> GetGitCommandOutput();
}