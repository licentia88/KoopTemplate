using System.Security.Claims;
using KoopTemplate.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoopTemplate.Web.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly IAdAuthenticator _auth;
    private readonly IConfiguration _configuration;

    public AuthController(IAdAuthenticator auth, IConfiguration configuration)
    {
        _auth = auth;
        _configuration = configuration;
    }
 
    [HttpPost("login")]
    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginRequest login, [FromQuery] string? returnUrl = null)
    {
        _auth.LdapServer = _configuration.GetValue<string>("LdapSettings:LdapServer");

        if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
            return LocalRedirect("/login?error=missing");

        if (!_auth.Validate(login.Username, login.Password, _auth.LdapServer))
            return LocalRedirect("/login?error=invalid");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, login.Username),
            new Claim(ClaimTypes.NameIdentifier, login.Username)
        };
        var principal = new ClaimsPrincipal(
            new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties { IsPersistent = false, ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8) });

        // Respect ReturnUrl if present (added by Cookie middleware)
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);

        return LocalRedirect("/");
    }

    [HttpPost("logout")]
    [Authorize]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect("/login");
    }

    [HttpGet("ping")]
    [AllowAnonymous]
    public IActionResult Ping()
    {
        Console.WriteLine("Checking auth via /auth/ping");
        return User.Identity?.IsAuthenticated == true
            ? Ok()
            : Unauthorized();
    }
}