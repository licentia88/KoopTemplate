using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using MudBlazor;

namespace KoopTemplate.Web.Theme;

[RegisterScoped]
public class AppThemeService : IAppThemeService
{
    private readonly IJSRuntime _jsRuntime;

    public MudTheme Theme { get; }
    public bool IsDarkMode { get; private set; }

    public AppThemeService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;

        Theme = new MudTheme();
        IsDarkMode = false;
    }

    public string DarkLightModeButtonIcon => IsDarkMode
        ? Icons.Material.Rounded.AutoMode
        : Icons.Material.Outlined.DarkMode;

    public async Task ToggleDarkModeAsync()
    {
        IsDarkMode = !IsDarkMode;

        try
        {
            await _jsRuntime.InvokeVoidAsync("setDarkMode", IsDarkMode);
        }
        catch
        {
            // JS tarafı yoksa bile uygulama çökmemeli.
        }
    }
}

