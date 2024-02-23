using EasyChangelogProd.Modules.AppSettings.Interfaces;

namespace EasyChangelogProd.Modules.GitCommand.Interfaces;

public interface IGitCommandAppSettings : ICommitTagTypes, IChangelogSettings
{
}