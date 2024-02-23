using EasyChangelogProd.Modules.AppSettings.Models;

namespace EasyChangelogProd.Modules.AppSettings.Interfaces;

public interface ICommitTagTypes
{
    List<CommitTagTypes__prune> GetCommitTagTypes();
}