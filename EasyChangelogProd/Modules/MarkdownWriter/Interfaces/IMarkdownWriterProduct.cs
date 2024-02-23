namespace EasyChangelogProd.Modules.MarkdownWriter.Interfaces;

public interface IMarkdownWriterProduct
{
    IMarkdownWriterProduct SetHeader();
    IMarkdownWriterProduct SetBody();
    IMarkdownWriterProduct SetFilePath();
    IMarkdownWriterProduct Build();
    void WriteFile();
}