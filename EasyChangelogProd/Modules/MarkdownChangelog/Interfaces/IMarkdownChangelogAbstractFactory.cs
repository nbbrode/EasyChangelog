namespace EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;

public interface IMarkdownChangelogAbstractFactory
{
    IBodyProduct BuildBodyProduct();
    IHeaderProduct BuildHeaderProduct();
}