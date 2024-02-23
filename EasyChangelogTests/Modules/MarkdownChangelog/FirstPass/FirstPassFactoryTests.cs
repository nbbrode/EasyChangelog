using EasyChangelogProd.Modules.AppSettings.Services;
using EasyChangelogProd.Modules.GitCommand.Services;
using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;
using EasyChangelogProd.Modules.MarkdownChangelog.Services.FirstPass;
using Microsoft.Extensions.Configuration;

namespace EasyChangelogTests.Modules.MarkdownChangelog.FirstPass;

[TestClass]
public class FirstPassFactoryTests
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
    public void BuildBodyProduct_ReturnAnIBodyProductType()
    {
        // Arrange
        var firstPassFactory =
            (IMarkdownChangelogAbstractFactory)new FirstPassFactory(_markdownChangelogAppSettings,
                _markdownChangelogGitCommand);

        // Act
        var result = firstPassFactory.BuildBodyProduct();

        // Assert
        Assert.IsInstanceOfType<IBodyProduct>(result);
    }

    [TestMethod]
    public void BuildHeaderProduct_ReturnAnIHeaderProductType()
    {
        // Arrange
        var firstPassFactory =
            (IMarkdownChangelogAbstractFactory)new FirstPassFactory(_markdownChangelogAppSettings,
                _markdownChangelogGitCommand);

        // Act
        var result = firstPassFactory.BuildHeaderProduct();

        // Assert
        Assert.IsInstanceOfType<IHeaderProduct>(result);
    }
}