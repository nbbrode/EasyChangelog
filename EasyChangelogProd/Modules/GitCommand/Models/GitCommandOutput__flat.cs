namespace EasyChangelogProd.Modules.GitCommand.Models;

public class GitCommandOutput__flat
{
    public DateTime DateOfCommit { get; set; }
    public DateTime ReportingDateOfCommit { get; set; }

    public string ConventionalCommitTag { get; set; }
    public string Version { get; set; }
    public string FullMessage { get; set; }

    public string MessageNoTag { get; set; }
    public string SectionTitle { get; set; }
}