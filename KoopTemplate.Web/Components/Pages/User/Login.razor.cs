using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;

namespace KoopTemplate.Web.Components.Pages.User;

public partial class Login
{
    private string _username = "";
    private string _password = "";
    private string _error;
    private string _formAction = "/auth/login";
    private bool _showPwd;

    private readonly Dictionary<string, object> _usernameAttrs = new()
    {
        ["name"] = "Username",
        ["autocomplete"] = "username"
    };

    private readonly Dictionary<string, object> _passwordAttrs = new()
    {
        ["name"] = "Password",
        ["autocomplete"] = "current-password"
    };

    protected override void OnInitialized()
    {
        var qs = QueryHelpers.ParseQuery(new Uri(Nav.Uri).Query);
        if (qs.TryGetValue("ReturnUrl", out var ret) && !string.IsNullOrWhiteSpace(ret))
            _formAction = $"/auth/login?ReturnUrl={Uri.EscapeDataString(ret!)}";
        if (qs.TryGetValue("error", out var e))
            _error = e == "invalid" ? "Kullanıcı Adı ve Şifre Hatalı."
                : e == "missing" ? "Lütfen Kullanıcı Adı ve Şifre giriniz."
                : e.ToString();
    }

    [CascadingParameter] private Task<AuthenticationState> AuthTask { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        var auth = await AuthTask;
        if (auth.User.Identity?.IsAuthenticated == true)
        {
            // Respect ReturnUrl (added by the cookie middleware), else go home
            var uri = new Uri(Nav.Uri);
            var qs = QueryHelpers.ParseQuery(uri.Query);
            var returnUrl = qs.TryGetValue("ReturnUrl", out var v) ? v.ToString() : "/";

            // avoid loops or odd targets
            if (string.IsNullOrWhiteSpace(returnUrl) ||
                returnUrl.Equals("/login", StringComparison.OrdinalIgnoreCase) ||
                returnUrl.StartsWith("/auth/", StringComparison.OrdinalIgnoreCase))
            {
                returnUrl = "/";
            }

            Nav.NavigateTo(returnUrl, forceLoad: true);
        }
    }
}