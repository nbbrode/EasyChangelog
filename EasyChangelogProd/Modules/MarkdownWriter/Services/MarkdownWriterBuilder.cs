using EasyChangelogProd.Modules.HostedService.Interfaces;
using EasyChangelogProd.Modules.MarkdownWriter.Interfaces;

namespace EasyChangelogProd.Modules.MarkdownWriter.Services;

public class MarkdownWriterBuilder(
    IMarkdownWriterMarkdownChangelog markdownWriterMarkdownChangelog,
    IMarkdownWriterAppSettings markdownWriterAppSettings) : IMarkdownWriterBuilder,
    IHostedServiceMarkdownWriter
{
    IMarkdownWriterProduct IMarkdownWriterBuilder.BuildMarkdownWriterProduct()
    {
        var results =
            ((IMarkdownWriterProduct)new MarkdownWriterProduct(markdownWriterMarkdownChangelog,
                markdownWriterAppSettings))
            .SetHeader()
            .SetBody()
            .SetFilePath()
            .Build();
        return results;
    }
}