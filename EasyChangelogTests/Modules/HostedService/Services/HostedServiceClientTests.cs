using EasyChangelogProd.Modules.HostedService.Interfaces;
using EasyChangelogProd.Modules.HostedService.Services;
using FakeItEasy;
using Microsoft.Extensions.Hosting;

namespace EasyChangelogTests.Modules.HostedService.Services;

[TestClass]
public class HostedServiceClientTests
{
    [TestMethod]
    public void StartAsync_ShouldCompleteSuccessfully()
    {
        // Arrange

        var appHostAppLifetime = A.Fake<IHostApplicationLifetime>();
        var hostedServiceMarkdownWriter = A.Fake<IHostedServiceMarkdownWriter>();

        var hostedService =
            (IHostedService)new HostedServiceClient(appHostAppLifetime,
                hostedServiceMarkdownWriter);
        var cancellationToken = new CancellationToken();

        // Act
        var result = hostedService.StartAsync(cancellationToken);

        // Assert
        Assert.AreEqual(TaskStatus.RanToCompletion, result.Status);
    }
}