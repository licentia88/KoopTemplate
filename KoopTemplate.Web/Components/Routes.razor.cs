using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace KoopTemplate.Web.Components;

public partial class Routes
{
    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationStateTask { get; set; }
    
    private bool IsAuthenticated { get; set; }
    public bool IsLoaded { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateTask;
        IsAuthenticated = authState.User.Identity?.IsAuthenticated ?? false;

        IsLoaded = true;
    }
}