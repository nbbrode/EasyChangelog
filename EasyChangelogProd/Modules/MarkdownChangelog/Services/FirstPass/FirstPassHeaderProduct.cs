using System.Text;
using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;

namespace EasyChangelogProd.Modules.MarkdownChangelog.Services.FirstPass;

public class FirstPassHeaderProduct(IMarkdownChangelogAppSettings markdownChangelogAppSettings) : IHeaderProduct
{
    private string? _appName;
    private string? _markdown;


    IHeaderProduct IHeaderProduct.SetAppName()
    {
        _appName = markdownChangelogAppSettings.GetChangelogSettingsPrune()
            .AppName;
        return this;
    }


    IHeaderProduct IHeaderProduct.Build()
    {
        if (_appName == null) throw new Exception("_appName cannot be null.");

        var sb = new StringBuilder();
        sb.AppendLine("# Changelog");
        sb.AppendLine(string.Empty);
        sb.AppendLine(string.Empty);
        sb.AppendLine(@$"All notable changes to `{_appName}` will be documented in this file.");
        sb.AppendLine(string.Empty);

        _markdown = sb.ToString();

        return this;
    }

    string IHeaderProduct.GetMarkdown()
    {
        return _markdown ?? string.Empty;
    }
}