namespace EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;

public interface IMarkdownChangelogFactoryProvider
{
    IMarkdownChangelogAbstractFactory CreateFirstPassFactory();
}