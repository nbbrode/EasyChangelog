using System.Reflection;
using EasyChangelogProd.Extensions;
using EasyChangelogProd.Modules.AppSettings.Services;
using EasyChangelogProd.Modules.GitCommand.Services;
using EasyChangelogProd.Modules.MarkdownChangelog.Services;
using EasyChangelogProd.Modules.MarkdownWriter.Interfaces;
using EasyChangelogProd.Modules.MarkdownWriter.Services;
using Microsoft.Extensions.Configuration;

namespace EasyChangelogTests.Modules.MarkdownWriter;

[TestClass]
public class MarkdownWriterProductTests
{
    private readonly IMarkdownWriterAppSettings _markdownWriterAppSettings;
    private readonly IMarkdownWriterMarkdownChangelog _markdownWriterMarkdownChangelog;

    public MarkdownWriterProductTests()
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
    public void SetHeader_ReturnsAnIMarkdownWriterProduct()
    {
        // Arrange
        var markdownWriterProduct =
            (IMarkdownWriterProduct)new MarkdownWriterProduct(_markdownWriterMarkdownChangelog,
                _markdownWriterAppSettings);

        // Act
        var result = markdownWriterProduct.SetHeader();

        // Assert
        Assert.IsInstanceOfType<IMarkdownWriterProduct>(result);
    }

    [TestMethod]
    public void SetBody_ReturnsAnIMarkdownWriterProduct()
    {
        // Arrange
        var markdownWriterProduct =
            (IMarkdownWriterProduct)new MarkdownWriterProduct(_markdownWriterMarkdownChangelog,
                _markdownWriterAppSettings);

        // Act
        var result = markdownWriterProduct
            .SetHeader()
            .SetBody();

        // Assert
        Assert.IsInstanceOfType<IMarkdownWriterProduct>(result);
    }

    [TestMethod]
    public void SetFilePath_ReturnsAnIMarkdownWriterProduct()
    {
        // Arrange
        var markdownWriterProduct =
            (IMarkdownWriterProduct)new MarkdownWriterProduct(_markdownWriterMarkdownChangelog,
                _markdownWriterAppSettings);

        // Act
        var result = markdownWriterProduct
            .SetHeader()
            .SetBody()
            .SetFilePath();

        // Assert
        Assert.IsInstanceOfType<IMarkdownWriterProduct>(result);
    }

    [TestMethod]
    public void Build_ReturnsAnIMarkdownWriterProduct()
    {
        // Arrange
        var markdownWriterProduct =
            (IMarkdownWriterProduct)new MarkdownWriterProduct(_markdownWriterMarkdownChangelog,
                _markdownWriterAppSettings);

        // Act
        var result = markdownWriterProduct
            .SetHeader()
            .SetBody()
            .SetFilePath()
            .Build();

        // Assert
        Assert.IsInstanceOfType<IMarkdownWriterProduct>(result);
    }


    [TestMethod]
    public void WriteFile_WritesMarkdownFileToCorrectLocation()
    {
        // Arrange
        var markdownWriterProduct =
            (IMarkdownWriterProduct)new MarkdownWriterProduct(_markdownWriterMarkdownChangelog,
                _markdownWriterAppSettings);

        // Act
        markdownWriterProduct
            .SetHeader()
            .SetBody()
            .SetFilePath()
            .Build()
            .WriteFile();

        var targetFolderName = _markdownWriterAppSettings.GetChangelogSettingsPrune().AppName;
        var rootProjectPath = string.Empty;
        if (targetFolderName != null)
            rootProjectPath = Assembly
                .GetExecutingAssembly()
                .GetExecutablePath()
                .FindMatchingParentFolder(targetFolderName);

        rootProjectPath += @"\CHANGELOG.md";

        // Assert 
        Assert.IsTrue(rootProjectPath.IsFileCreatedRecently(TimeSpan.FromSeconds(5)));
    }
}