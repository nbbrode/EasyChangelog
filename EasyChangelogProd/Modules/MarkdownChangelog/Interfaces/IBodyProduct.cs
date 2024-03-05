using System.Text;

namespace EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;

public interface IBodyProduct
{
    IBodyProduct SetGitLogOutput();
    IBodyProduct SetCommitTagTypes();

    IBodyProduct ScrubGitData();
    IBodyProduct Build();

    void BuildVersionTitles(StringBuilder sb);

    void BuildSectionTitles(StringBuilder sb, string versionKey);

    void BuildCommitMessages(StringBuilder sb, string versionKey, string sectionKey);


    string GetMarkdown();
}