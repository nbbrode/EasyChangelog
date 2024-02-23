namespace EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;

public interface IHeaderProduct
{
    IHeaderProduct SetAppName();
    IHeaderProduct Build();
    string GetMarkdown();
}