using EasyChangelogProd.Modules.AppSettings.Services;
using EasyChangelogProd.Modules.GitCommand.Services;
using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;
using EasyChangelogProd.Modules.MarkdownChangelog.Services;
using Microsoft.Extensions.Configuration;

namespace EasyChangelogTests.Modules.MarkdownChangelog;

[TestClass]
public class MarkdownChangelogFactoryProviderTests
{
    private readonly IMarkdownChangelogAppSettings _markdownChangelogAppSettings = new AppSettingsClient(
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build());

    private readonly IMarkdownChangelogGitCommand _markdownChangelogGitCommand =
        new GitCommandFactory(new AppSettingsClient(
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()));


    [TestMethod]
    public void CreateFirstPassFactory_ReturnsIMarkdownChangelogAbstractFactory()
    {
        // Arrange
        var markdownChangelogFactoryProvider =
            (IMarkdownChangelogFactoryProvider)new MarkdownChangelogFactoryProvider(_markdownChangelogAppSettings,
                _markdownChangelogGitCommand);

        // Act
        var result = markdownChangelogFactoryProvider.CreateFirstPassFactory();

        // Assert
        Assert.IsInstanceOfType<IMarkdownChangelogAbstractFactory>(result);
    }
}