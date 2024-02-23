using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;

namespace EasyChangelogProd.Modules.MarkdownChangelog.Services.FirstPass;

public class FirstPassFactory(
    IMarkdownChangelogAppSettings markdownChangelogAppSettings,
    IMarkdownChangelogGitCommand markdownChangelogGitCommand) : IMarkdownChangelogAbstractFactory
{
    IBodyProduct IMarkdownChangelogAbstractFactory.BuildBodyProduct()
    {
        return ((IBodyProduct)new FirstPassBodyProduct(markdownChangelogAppSettings, markdownChangelogGitCommand))
            .SetGitLogOutput()
            .SetCommitTagTypes()
            .ScrubGitData()
            .Build();
    }

    IHeaderProduct IMarkdownChangelogAbstractFactory.BuildHeaderProduct()
    {
        return ((IHeaderProduct)new FirstPassHeaderProduct(markdownChangelogAppSettings))
            .SetAppName()
            .Build();
    }
}