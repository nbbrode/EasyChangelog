using EasyChangelogProd.Modules.AppSettings.Services;
using EasyChangelogProd.Modules.GitCommand.Interfaces;
using EasyChangelogProd.Modules.GitCommand.Models;
using EasyChangelogProd.Modules.GitCommand.Services;
using Microsoft.Extensions.Configuration;

namespace EasyChangelogTests.Modules.GitCommand;

[TestClass]
public class GitCommandProductTests
{
    private readonly IGitCommandAppSettings _gitCommandAppSettings = new AppSettingsClient(
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build());

    [TestMethod]
    public void SetChangelogSettingsPrune_ReturnAnIGitCommandProductType()
    {
        // Arrange
        var gitCommandProduct = (IGitCommandProduct)new GitCommandOutputQ1Product(_gitCommandAppSettings);

        // Act
        var result = gitCommandProduct.SetChangelogSettingsPrune();

        // Assert
        Assert.IsInstanceOfType<IGitCommandProduct>(result);
    }

    [TestMethod]
    public void SetGitExePath_ReturnAnIGitCommandProductType()
    {
        // Arrange
        var gitCommandProduct = (IGitCommandProduct)new GitCommandOutputQ1Product(_gitCommandAppSettings);

        // Act
        var result = gitCommandProduct
            .SetChangelogSettingsPrune()
            .SetGitExePath();

        // Assert
        Assert.IsInstanceOfType<IGitCommandProduct>(result);
    }

    [TestMethod]
    public void SetGitRepoPath_ReturnAnIGitCommandProductType()
    {
        // Arrange
        var gitCommandProduct = (IGitCommandProduct)new GitCommandOutputQ1Product(_gitCommandAppSettings);

        // Act
        var result = gitCommandProduct
            .SetChangelogSettingsPrune()
            .SetGitExePath()
            .SetGitRepoPath();

        // Assert
        Assert.IsInstanceOfType<IGitCommandProduct>(result);
    }

    [TestMethod]
    public void SetProcessStartInfoArgs_ReturnAnIGitCommandProductType()
    {
        // Arrange
        var gitCommandProduct = (IGitCommandProduct)new GitCommandOutputQ1Product(_gitCommandAppSettings);

        // Act
        var result = gitCommandProduct
            .SetChangelogSettingsPrune()
            .SetGitExePath()
            .SetGitRepoPath()
            .SetProcessStartInfoArgs();

        // Assert
        Assert.IsInstanceOfType<IGitCommandProduct>(result);
    }

    [TestMethod]
    public void Build_ReturnAnIGitCommandProductType()
    {
        // Arrange
        var gitCommandProduct = (IGitCommandProduct)new GitCommandOutputQ1Product(_gitCommandAppSettings);

        // Act
        var result = gitCommandProduct
            .SetChangelogSettingsPrune()
            .SetGitExePath()
            .SetGitRepoPath()
            .SetProcessStartInfoArgs()
            .Build();

        // Assert
        Assert.IsInstanceOfType<IGitCommandProduct>(result);
    }

    [TestMethod]
    public void GetGitCommandOutput_ReturnAListOfGetGitCommandOutput()
    {
        // Arrange
        var gitCommandProduct = (IGitCommandProduct)new GitCommandOutputQ1Product(_gitCommandAppSettings);

        // Act
        var result = gitCommandProduct
            .SetChangelogSettingsPrune()
            .SetGitExePath()
            .SetGitRepoPath()
            .SetProcessStartInfoArgs()
            .Build()
            .GetGitCommandOutput();

        // Assert
        Assert.IsInstanceOfType<List<GitCommandOutput__flat>>(result);
    }
}