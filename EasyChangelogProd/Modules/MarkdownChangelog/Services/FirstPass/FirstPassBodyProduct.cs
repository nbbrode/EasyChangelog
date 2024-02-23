using System.Diagnostics;
using System.Text;
using EasyChangelogProd.Modules.AppSettings.Models;
using EasyChangelogProd.Modules.GitCommand.Models;
using EasyChangelogProd.Modules.MarkdownChangelog.Interfaces;

namespace EasyChangelogProd.Modules.MarkdownChangelog.Services.FirstPass;

public class FirstPassBodyProduct(
    IMarkdownChangelogAppSettings markdownChangelogAppSettings,
    IMarkdownChangelogGitCommand markdownChangelogGitCommand) : IBodyProduct
{
    private List<CommitTagTypes__prune>? _commitTagTypesPruneList;
    private List<GitCommandOutput__flat>? _gitLogOutputFlatList;
    private string? _markdown;
    private List<GitCommandOutput__flat>? _scrubbedLogOutputFlatList;

    IBodyProduct IBodyProduct.SetGitLogOutput()
    {
        _gitLogOutputFlatList = markdownChangelogGitCommand
                                    .BuildGitCommandOutputQ1Product()
                                    .GetGitCommandOutput()
                                ?? throw new Exception("There is no _gitLogOutput1FlatList!");


        return this;
    }

    IBodyProduct IBodyProduct.SetCommitTagTypes()
    {
        _commitTagTypesPruneList = markdownChangelogAppSettings
            .GetCommitTagTypes();
        return this;
    }

    IBodyProduct IBodyProduct.ScrubGitData()
    {
        if (_gitLogOutputFlatList == null) throw new Exception("_gitLogOutput should not be null");

        if (_commitTagTypesPruneList == null) throw new Exception("_commitTypes should not be null");

        _scrubbedLogOutputFlatList = new List<GitCommandOutput__flat>();

        foreach (var i in _gitLogOutputFlatList)
        {
            var tagCount = _commitTagTypesPruneList
                .Count(x => x.CommitTagTypeName != null && x.CommitTagTypeName
                    .Equals(i.ConventionalCommitTag) && !x.IsHidden);

            var reportableExists = Convert.ToBoolean(tagCount);

            if (reportableExists)
            {
                var sectionTitle = _commitTagTypesPruneList
                    .Where(x => x.CommitTagTypeName != null
                                && x.CommitTagTypeName.Equals(i.ConventionalCommitTag)
                                && !x.IsHidden).Select(x => x.SectionTitle)
                    .Single();

                if (sectionTitle == null) throw new Exception("sectionTitle cannot be null");

                _scrubbedLogOutputFlatList.Add(new GitCommandOutput__flat
                {
                    ConventionalCommitTag = i.ConventionalCommitTag,
                    ReportingDateOfCommit = Convert.ToDateTime(i.DateOfCommit.ToString("MM-dd-yyyy")),
                    DateOfCommit = i.DateOfCommit,
                    MessageNoTag = i.MessageNoTag,
                    SectionTitle = sectionTitle,
                    Version = i.Version?.Split('-')[0].Trim()
                });
            }
        }

        _scrubbedLogOutputFlatList = _scrubbedLogOutputFlatList
            .OrderByDescending(x => x.DateOfCommit)
            .ToList();

        return this;
    }

    void IBodyProduct.BuildCommitMessages(StringBuilder sb, string versionKey, string? sectionKey)
    {
        var commitMessages = _scrubbedLogOutputFlatList?
            .Where(x => x is { ConventionalCommitTag: not null, Version: not null }
                        && x.Version.Equals(versionKey)
                        && x.ConventionalCommitTag.Equals(sectionKey))
            .Select(x => x.MessageNoTag)
            .ToList();

        if (commitMessages == null) return;
        foreach (var s in commitMessages)
        {
            sb.AppendLine($"- {s}");
            Debug.WriteLine(s);
        }
    }

    void IBodyProduct.BuildSectionTitles(StringBuilder sb, string versionKey)
    {
        var sections = _scrubbedLogOutputFlatList?
            .Where(x => x is { Version: not null }
                        && x.Version.Equals(versionKey))
            .Select(x => new
            {
                x.ConventionalCommitTag,
                x.SectionTitle
            })
            .Distinct();

        if (sections == null) return;
        foreach (var anonT in sections)
        {
            Debug.WriteLine(anonT.SectionTitle);
            sb.AppendLine($"### {anonT.SectionTitle}");
            ((IBodyProduct)this).BuildCommitMessages(sb, versionKey, anonT.ConventionalCommitTag);
        }
    }

    void IBodyProduct.BuildVersionTitles(StringBuilder sb)
    {
        var versions = _scrubbedLogOutputFlatList?
            .Select(x => x.Version)
            .Distinct();

        if (versions == null) return;

        foreach (var v in versions)
        {
            sb.AppendLine($"## Version: {v}");

            if (_scrubbedLogOutputFlatList == null) return;
            var minDate = _scrubbedLogOutputFlatList
                .Where(x => x.Version != null && x.Version.Equals(v))
                .Select(x => x.ReportingDateOfCommit)
                .Min();


            var maxDate = _scrubbedLogOutputFlatList
                .Where(x => x.Version != null && x.Version.Equals(v))
                .Select(x => x.ReportingDateOfCommit)
                .Max();

            sb.AppendLine($"Dates: {minDate.ToString("MM/dd/yyyy")} To {maxDate.ToString("MM/dd/yyyy")}");
            sb.AppendLine(string.Empty);
            if (v != null) ((IBodyProduct)this).BuildSectionTitles(sb, v);
        }
    }


    IBodyProduct IBodyProduct.Build()
    {
        // this generates markdown
        if (_scrubbedLogOutputFlatList == null) throw new Exception("_scrubbedLogOutputFlatList cannot be null");

        var sb = new StringBuilder();
        ((IBodyProduct)this).BuildVersionTitles(sb);

        _markdown = sb.ToString();
        return this;
    }


    string IBodyProduct.GetMarkdown()
    {
        if (string.IsNullOrEmpty(_markdown)) throw new Exception("_markdown cannot be null or empty");

        return _markdown;
    }
}