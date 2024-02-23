using System.Diagnostics;
using EasyChangelogProd.Modules.AppSettings.Services;
using EasyChangelogProd.Modules.GitCommand.Interfaces;
using EasyChangelogProd.Modules.GitCommand.Services;
using EasyChangelogProd.Modules.HostedService.Interfaces;
using EasyChangelogProd.Modules.HostedService.Services;
using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;
using EasyChangelogProd.Modules.MarkdownChangelog.Services;
using EasyChangelogProd.Modules.MarkdownWriter.Interfaces;
using EasyChangelogProd.Modules.MarkdownWriter.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var cancellationTokenSource = new CancellationTokenSource();
// Get the cancellation token from the source
var cancellationToken = cancellationTokenSource.Token;

if (args.Length != 0 && args[0].Equals("cancel")) cancellationTokenSource.Cancel();

try
{
    await Host.CreateDefaultBuilder(args)
        .ConfigureHostConfiguration(configHost => { configHost.AddJsonFile("appsettings.json"); })
        .ConfigureServices((hostContext, services) =>
        {
            var serviceCollection = services
                .Configure<ConsoleLifetimeOptions>(opts => opts.SuppressStatusMessages = true)
                .AddTransient<IGitCommandAppSettings, AppSettingsClient>()
                .AddTransient<IMarkdownChangelogAppSettings, AppSettingsClient>()
                .AddTransient<IMarkdownChangelogGitCommand, GitCommandFactory>()
                .AddTransient<IMarkdownWriterAppSettings, AppSettingsClient>()
                .AddTransient<IMarkdownWriterMarkdownChangelog, MarkdownChangelogFactoryProvider>()
                .AddTransient<IHostedServiceMarkdownWriter, MarkdownWriterBuilder>()
                .AddHostedService<HostedServiceClient>();
        }).RunConsoleAsync(cancellationToken);
}
catch (OperationCanceledException)
{
    Debug.Write("The operation was canceled");
}