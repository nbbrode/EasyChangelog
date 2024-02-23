using EasyChangelogProd.Modules.AppSettings.Interfaces;
using EasyChangelogProd.Modules.AppSettings.Models;
using EasyChangelogProd.Modules.AppSettings.Services;
using Microsoft.Extensions.Configuration;

namespace EasyChangelogTests.Modules.AppSettings;

[TestClass]
public class AppSettingsClientTests
{
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    [TestMethod]
    public void GetChangelogSettingsPrune_ReturnsAChangelogSettingsPrune()
    {
        // Arrange
        var appSettingsClient = (IChangelogSettings)new AppSettingsClient(_configuration);

        // Act
        var result = appSettingsClient.GetChangelogSettingsPrune();

        // Assert
        Assert.IsInstanceOfType<ChangelogSettings__prune>(result);
    }

    [TestMethod]
    public void GetCommitTagTypes_ReturnsListOfCommitTagTypesPrune()
    {
        // Arrange
        var appSettingsClient = (ICommitTagTypes)new AppSettingsClient(_configuration);

        // Act
        var result = appSettingsClient.GetCommitTagTypes();

        // Assert
        Assert.IsInstanceOfType<List<CommitTagTypes__prune>>(result);
    }
}