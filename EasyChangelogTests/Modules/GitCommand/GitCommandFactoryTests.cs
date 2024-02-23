using EasyChangelogProd.Modules.AppSettings.Services;
using EasyChangelogProd.Modules.GitCommand.Interfaces;
using EasyChangelogProd.Modules.GitCommand.Services;
using Microsoft.Extensions.Configuration;

namespace EasyChangelogTests.Modules.GitCommand;

[TestClass]
public class GitCommandFactoryTests
{
    private readonly IGitCommandAppSettings _gitCommandAppSettings = new AppSettingsClient(
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build());

    [TestMethod]
    public void BuildGitCommandOutputQ1Product_ReturnAnIGitCommandProductType()
    {
        // Arrange
        var gitCommandFactory =
            (IGitCommandFactory)new GitCommandFactory(_gitCommandAppSettings);

        // Act
        var result = gitCommandFactory.BuildGitCommandOutputQ1Product();

        // Assert
        Assert.IsInstanceOfType<IGitCommandProduct>(result);
    }
}