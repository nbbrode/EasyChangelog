using EasyChangelogProd.Modules.HostedService.Interfaces;
using Microsoft.Extensions.Hosting;

namespace EasyChangelogProd.Modules.HostedService.Services;

public class HostedServiceClient(
    IHostApplicationLifetime hostApplicationLifetime,
    IHostedServiceMarkdownWriter hostedServiceMarkdownWriter) : IHostedService
{
    Task IHostedService.StartAsync(CancellationToken cancellationToken)
    {
        hostApplicationLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(() =>
            {
                hostedServiceMarkdownWriter
                    .BuildMarkdownWriterProduct()
                    .WriteFile();

                hostApplicationLifetime.StopApplication();


                return Task.CompletedTask;
            }, cancellationToken);
        });

        return Task.CompletedTask;
    }

    Task IHostedService.StopAsync(CancellationToken cancellationToken)
    {
        //throw new NotSupportedException();
        return Task.CompletedTask;
    }
}