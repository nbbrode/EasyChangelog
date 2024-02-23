using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;
using EasyChangelogProd.Modules.MarkdownChangelog.Services.FirstPass;
using EasyChangelogProd.Modules.MarkdownWriter.Interfaces;

namespace EasyChangelogProd.Modules.MarkdownChangelog.Services;

public class MarkdownChangelogFactoryProvider(
    IMarkdownChangelogAppSettings markdownChangelogAppSettings,
    IMarkdownChangelogGitCommand markdownChangelogGitCommand)
    : IMarkdownChangelogFactoryProvider, IMarkdownWriterMarkdownChangelog
{
    IMarkdownChangelogAbstractFactory IMarkdownChangelogFactoryProvider.CreateFirstPassFactory()
    {
        return new FirstPassFactory(markdownChangelogAppSettings, markdownChangelogGitCommand);
    }
}