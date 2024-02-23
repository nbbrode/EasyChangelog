using EasyChangelogProd.Modules.AppSettings.Services;
using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;
using EasyChangelogProd.Modules.MarkdownChangelog.Services.FirstPass;
using Microsoft.Extensions.Configuration;

namespace EasyChangelogTests.Modules.MarkdownChangelog.FirstPass;

[TestClass]
public class FirstPassHeaderProductTests
{
    private readonly IMarkdownChangelogAppSettings _markdownChangelogAppSettings = new AppSettingsClient(
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build());


    [TestMethod]
    public void SetAppName_ReturnAnIHeaderProductType()
    {
        // Arrange
        var firstPassHeaderProduct =
            (IHeaderProduct)new FirstPassHeaderProduct(_markdownChangelogAppSettings);

        // Act
        var result = firstPassHeaderProduct.SetAppName();

        // Assert
        Assert.IsInstanceOfType<IHeaderProduct>(result);
    }

    [TestMethod]
    public void Build_ReturnAnIHeaderProductType()
    {
        // Arrange
        var firstPassHeaderProduct =
            (IHeaderProduct)new FirstPassHeaderProduct(_markdownChangelogAppSettings);

        // Act
        var result = firstPassHeaderProduct
            .SetAppName()
            .Build();

        // Assert
        Assert.IsInstanceOfType<IHeaderProduct>(result);
    }

    [TestMethod]
    public void GetMarkdown_ReturnMarkdownSample()
    {
        // Arrange
        var firstPassHeaderProduct =
            (IHeaderProduct)new FirstPassHeaderProduct(_markdownChangelogAppSettings);

        // Act
        var result = firstPassHeaderProduct
            .SetAppName()
            .Build()
            .GetMarkdown();

        // Assert
        Assert.IsTrue(result.Contains("# Changelog"));
    }
}