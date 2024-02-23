#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace EasyChangelogProd.Modules.AppSettings.Models;

public class AppSettings__root
{
    public Logging Logging { get; set; }
    public Myappsettings MyAppSettings { get; set; }
    public string[] IgnoredCommitHashes { get; set; }
    public Changelogsettings ChangelogSettings { get; set; }
    public Committagtype[] CommitTagTypes { get; set; }
}

public class Logging
{
    public Loglevel LogLevel { get; set; }
}

public class Loglevel
{
    public string Default { get; set; }
    public string Microsoft { get; set; }
    public string MicrosoftHostingLifetime { get; set; }
}

public class Myappsettings
{
    public string ConnectionString { get; set; }
    public int MaxRetryAttempts { get; set; }
    public bool FeatureToggle { get; set; }
}

public class Changelogsettings
{
    public string AppName { get; set; }
    public string GitExePath { get; set; }
    public string ProcessStartInfoArgs { get; set; }
}

public class Committagtype
{
    public string CommitTagTypeName { get; set; }
    public bool IsHidden { get; set; }
    public string SectionTitle { get; set; }
}