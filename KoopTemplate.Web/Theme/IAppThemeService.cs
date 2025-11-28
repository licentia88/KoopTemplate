using MudBlazor;

namespace KoopTemplate.Web.Theme;

public interface IAppThemeService
{
    MudTheme Theme { get; }
    bool IsDarkMode { get; }
    string DarkLightModeButtonIcon { get; }
    Task ToggleDarkModeAsync();
}