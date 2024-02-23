using System.Reflection;
using System.Text;
using EasyChangelogProd.Extensions;
using EasyChangelogProd.Modules.MarkdownWriter.Interfaces;

namespace EasyChangelogProd.Modules.MarkdownWriter.Services;

public class MarkdownWriterProduct(
    IMarkdownWriterMarkdownChangelog markdownWriterMarkdownChangelog,
    IMarkdownWriterAppSettings markdownWriterAppSettings)
    : IMarkdownWriterProduct
{
    private string? _body;
    private string? _header;
    private string? _markdownFileContent;
    private string? _rootProjectPath;

    IMarkdownWriterProduct IMarkdownWriterProduct.SetHeader()
    {
        _header = markdownWriterMarkdownChangelog
            .CreateFirstPassFactory()
            .BuildHeaderProduct()
            .GetMarkdown();
        return this;
    }

    IMarkdownWriterProduct IMarkdownWriterProduct.SetBody()
    {
        _body = markdownWriterMarkdownChangelog
            .CreateFirstPassFactory()
            .BuildBodyProduct()
            .GetMarkdown();
        return this;
    }

    IMarkdownWriterProduct IMarkdownWriterProduct.SetFilePath()
    {
        var targetFolderName = markdownWriterAppSettings.GetChangelogSettingsPrune().AppName;
        if (targetFolderName != null)
            _rootProjectPath = Assembly
                .GetExecutingAssembly()
                .GetExecutablePath()
                .FindMatchingParentFolder(targetFolderName);

        _rootProjectPath += @"\CHANGELOG.md";

        return this;
    }


    IMarkdownWriterProduct IMarkdownWriterProduct.Build()
    {
        var sb = new StringBuilder();
        sb.Append(_header);
        sb.Append(_body);
        _markdownFileContent = sb.ToString();
        return this;
    }

    void IMarkdownWriterProduct.WriteFile()
    {
        if (_rootProjectPath == null) return;
        if (File.Exists(_rootProjectPath)) File.Delete(_rootProjectPath);
        using var sw = new StreamWriter(_rootProjectPath);
        sw.Write(_markdownFileContent);
    }
}