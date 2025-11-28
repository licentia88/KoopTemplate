using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using KoopTemplate.Web.Theme;

namespace KoopTemplate.Web.Components.Layout;

public partial class MainLayout
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IJSRuntime Js { get; set; }

    [Inject]
    public IAppThemeService ThemeService { get; set; }

    private bool _drawerOpen = true;
 
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await Js.InvokeVoidAsync("startAuthMonitor");
    }

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    private async Task DarkModeToggle()
    {
        await ThemeService.ToggleDarkModeAsync();
    }

    public string DarkLightModeButtonIcon => ThemeService.DarkLightModeButtonIcon;
}