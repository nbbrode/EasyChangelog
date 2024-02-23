using EasyChangelogProd.Modules.AppSettings.Models;

namespace EasyChangelogProd.Modules.AppSettings.Interfaces;

public interface IChangelogSettings
{
    public ChangelogSettings__prune GetChangelogSettingsPrune();
}