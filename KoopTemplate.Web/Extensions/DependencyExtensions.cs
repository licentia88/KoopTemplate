using KoopTemplate.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace KoopTemplate.Web.Extensions;

public static class DependencyExtensions
{
    public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
    {
        // AuthN/Z
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/denied";
                options.LogoutPath = "/logout";
                options.SlidingExpiration = false;
                options.ExpireTimeSpan = TimeSpan.FromHours(8);

                // Cookie hardening
                options.Cookie.Name = "__Host-PortalAuth";        // __Host- prefix requires Secure, Path=/, no Domain
                options.Cookie.Path = "/";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;       // use Strict if you never navigate from external origins
                options.Cookie.IsEssential = true;


                // Optional: avoid redirect loops for non-HTML requests
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (IsApiOrFetch(ctx.Request)) { ctx.Response.StatusCode = StatusCodes.Status401Unauthorized; return Task.CompletedTask; }
                        ctx.Response.Redirect(ctx.RedirectUri); return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = ctx =>
                    {
                        if (IsApiOrFetch(ctx.Request)) { ctx.Response.StatusCode = StatusCodes.Status403Forbidden; return Task.CompletedTask; }
                        ctx.Response.Redirect(ctx.RedirectUri); return Task.CompletedTask;
                    }
                };
                static bool IsApiOrFetch(HttpRequest r) =>
                    r.Path.StartsWithSegments("/api") ||
                    string.Equals(r.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase) ||
                    r.Headers.Accept.Any(h => h.Contains("application/json", StringComparison.OrdinalIgnoreCase));

            });

        return services;
    }

    public static WebApplicationBuilder AddAuthenticator(this WebApplicationBuilder builder)
    {
        // AD authenticator service
        builder.Services.AddSingleton<IAdAuthenticator>(sp =>
            new AdAuthenticator(
                domain: builder.Configuration["Auth:Domain"] ?? "YOURDOMAIN",
                containerOrServer: builder.Configuration["Auth:ContainerOrServer"] // e.g. "DC=yourdomain,DC=local" or null
            ));
        
         
        return builder;
    }
}