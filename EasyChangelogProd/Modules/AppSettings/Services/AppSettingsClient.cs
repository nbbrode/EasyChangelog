using EasyChangelogProd.Modules.AppSettings.Interfaces;
using EasyChangelogProd.Modules.AppSettings.Models;
using EasyChangelogProd.Modules.GitCommand.Interfaces;
using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;
using EasyChangelogProd.Modules.MarkdownWriter.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EasyChangelogProd.Modules.AppSettings.Services;

public class AppSettingsClient(IConfiguration configuration)
    : IGitCommandAppSettings, IMarkdownChangelogAppSettings, IMarkdownWriterAppSettings
{
    ChangelogSettings__prune IChangelogSettings.GetChangelogSettingsPrune()
    {
        var appSettingsRoot = new AppSettings__root();
        configuration.Bind(appSettingsRoot);


        return new ChangelogSettings__prune
        {
            AppName = appSettingsRoot.ChangelogSettings.AppName,
            GitExePath = appSettingsRoot.ChangelogSettings.GitExePath,
            ProcessStartInfoArgs = appSettingsRoot.ChangelogSettings.ProcessStartInfoArgs
        };
    }

    List<CommitTagTypes__prune> ICommitTagTypes.GetCommitTagTypes()
    {
        var appSettings = new AppSettings__root();
        configuration.Bind(appSettings);

        var results = new List<CommitTagTypes__prune>();
        foreach (var commitType in appSettings.CommitTagTypes)
            results.Add(new CommitTagTypes__prune
            {
                CommitTagTypeName = commitType.CommitTagTypeName,
                IsHidden = commitType.IsHidden,
                SectionTitle = commitType.SectionTitle
            });

        return results;
    }
}