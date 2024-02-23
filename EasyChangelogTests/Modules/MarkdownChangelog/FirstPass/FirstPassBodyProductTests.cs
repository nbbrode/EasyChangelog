using System.Text;
using EasyChangelogProd.Modules.AppSettings.Services;
using EasyChangelogProd.Modules.GitCommand.Services;
using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;
using EasyChangelogProd.Modules.MarkdownChangelog.Services.FirstPass;
using Microsoft.Extensions.Configuration;

namespace EasyChangelogTests.Modules.MarkdownChangelog.FirstPass;

[TestClass]
public class FirstPassBodyProductTests
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
    public void SetGitLogOutput_ReturnAnIBodyProductType()
    {
        // Arrange
        var firstPassBody =
            (IBodyProduct)new FirstPassBodyProduct(_markdownChangelogAppSettings,
                _markdownChangelogGitCommand);

        // Act
        var result = firstPassBody.SetGitLogOutput();

        // Assert
        Assert.IsInstanceOfType<IBodyProduct>(result);
    }

    [TestMethod]
    public void SetCommitTagTypes_ReturnAnIBodyProductType()
    {
        // Arrange
        var firstPassBody =
            (IBodyProduct)new FirstPassBodyProduct(_markdownChangelogAppSettings,
                _markdownChangelogGitCommand);

        // Act
        var result = firstPassBody
            .SetGitLogOutput()
            .SetCommitTagTypes();

        // Assert
        Assert.IsInstanceOfType<IBodyProduct>(result);
    }

    [TestMethod]
    public void ScrubGitData_ReturnAnIBodyProductType()
    {
        // Arrange
        var firstPassBody =
            (IBodyProduct)new FirstPassBodyProduct(_markdownChangelogAppSettings,
                _markdownChangelogGitCommand);

        // Act
        var result = firstPassBody
            .SetGitLogOutput()
            .SetCommitTagTypes()
            .ScrubGitData();

        // Assert
        Assert.IsInstanceOfType<IBodyProduct>(result);
    }

    [TestMethod]
    public void BuildCommitMessages_StringBuilderHasCommitMessageInfo()
    {
        // Arrange
        var firstPassBody =
            (IBodyProduct)new FirstPassBodyProduct(_markdownChangelogAppSettings,
                _markdownChangelogGitCommand);
        var stringBuilder = new StringBuilder();

        // Act
        firstPassBody
            .SetGitLogOutput()
            .SetCommitTagTypes()
            .ScrubGitData()
            .BuildCommitMessages(stringBuilder, "v1.0", "feat");

        // Assert
        Assert.IsTrue(stringBuilder.ToString().Contains("make app configurable"));
    }

    [TestMethod]
    public void BuildSectionTitles_StringBuilderHasSectionInfo()
    {
        // Arrange
        var firstPassBody =
            (IBodyProduct)new FirstPassBodyProduct(_markdownChangelogAppSettings,
                _markdownChangelogGitCommand);
        var stringBuilder = new StringBuilder();

        // Act
        firstPassBody
            .SetGitLogOutput()
            .SetCommitTagTypes()
            .ScrubGitData()
            .BuildSectionTitles(stringBuilder, "v1.0");

        // Assert
        Assert.IsTrue(stringBuilder.ToString().Contains("### Features"));
    }


    [TestMethod]
    public void BuildVersionTitles_StringBuilderHasVersionInfo()
    {
        // Arrange
        var firstPassBody =
            (IBodyProduct)new FirstPassBodyProduct(_markdownChangelogAppSettings,
                _markdownChangelogGitCommand);
        var stringBuilder = new StringBuilder();

        // Act
        firstPassBody
            .SetGitLogOutput()
            .SetCommitTagTypes()
            .ScrubGitData()
            .BuildVersionTitles(stringBuilder);

        // Assert
        Assert.IsTrue(stringBuilder.ToString().Contains("Version: v1.0"));
    }


    [TestMethod]
    public void Build_ReturnAnIBodyProductType()
    {
        // Arrange
        var firstPassBody =
            (IBodyProduct)new FirstPassBodyProduct(_markdownChangelogAppSettings,
                _markdownChangelogGitCommand);

        // Act
        var result = firstPassBody
            .SetGitLogOutput()
            .SetCommitTagTypes()
            .ScrubGitData()
            .Build();

        // Assert
        Assert.IsInstanceOfType<IBodyProduct>(result);
    }

    [TestMethod]
    public void GetMarkdown_ReturnMarkdownSample()
    {
        // Arrange
        var firstPassBody =
            (IBodyProduct)new FirstPassBodyProduct(_markdownChangelogAppSettings,
                _markdownChangelogGitCommand);

        // Act
        var result = firstPassBody
            .SetGitLogOutput()
            .SetCommitTagTypes()
            .ScrubGitData()
            .Build()
            .GetMarkdown();

        // Assert
        Assert.IsTrue(result.Contains("## Version: v1.0"));
    }
}