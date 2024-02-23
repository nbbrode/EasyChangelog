using EasyChangelogProd.Modules.AppSettings.Services;
using EasyChangelogProd.Modules.GitCommand.Services;
using EasyChangelogProd.Modules.MarkdownChangelog.Services;
using EasyChangelogProd.Modules.MarkdownWriter.Interfaces;
using EasyChangelogProd.Modules.MarkdownWriter.Services;
using Microsoft.Extensions.Configuration;

namespace EasyChangelogTests.Modules.MarkdownWriter;

[TestClass]
public class MarkdownWriterBuilderTests
{
    private readonly IMarkdownWriterAppSettings _markdownWriterAppSettings;
    private readonly IMarkdownWriterMarkdownChangelog _markdownWriterMarkdownChangelog;

    public MarkdownWriterBuilderTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var appSettingsClient = new AppSettingsClient(configuration);
        _markdownWriterAppSettings = appSettingsClient;
        var markdownChangelogGitCommand = new GitCommandFactory(appSettingsClient);
        _markdownWriterMarkdownChangelog =
            new MarkdownChangelogFactoryProvider(appSettingsClient, markdownChangelogGitCommand);
    }

    [TestMethod]
    public void BuildMarkdownWriterProduct_ReturnsAnIMarkdownWriterProduct()
    {
        // Arrange
        var markdownWriterBuilder =
            (IMarkdownWriterBuilder)new MarkdownWriterBuilder(_markdownWriterMarkdownChangelog,
                _markdownWriterAppSettings);

        // Act
        var result = markdownWriterBuilder.BuildMarkdownWriterProduct();

        // Assert
        Assert.IsInstanceOfType<IMarkdownWriterProduct>(result);
    }
}