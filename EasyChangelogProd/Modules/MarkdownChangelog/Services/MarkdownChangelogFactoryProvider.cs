using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;
using EasyChangelogProd.Modules.MarkdownChangelog.Services.FirstPass;

namespace EasyChangelogProd.Modules.MarkdownChangelog.Services;

public class MarkdownChangelogFactoryProvider(
    IMarkdownChangelogAppSettings markdownChangelogAppSettings,
    IMarkdownChangelogGitCommand markdownChangelogGitCommand)
    : IMarkdownChangelogFactoryProvider
{
    IMarkdownChangelogAbstractFactory IMarkdownChangelogFactoryProvider.CreateFirstPassFactory()
    {
        return new FirstPassFactory(markdownChangelogAppSettings, markdownChangelogGitCommand);
    }
}