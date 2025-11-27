using KoopTemplate.Web.Components;
using KoopTemplate.Web.Extensions;
using KoopTemplate.Web.Models;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<LdapSettings>(builder.Configuration.GetSection("LdapSettings"));




// UI & components
builder.Services.AddMudServices();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCookieAuthentication();

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

// Antiforgery (nice to standardize the header if you later use tokens in forms)
builder.Services.AddAntiforgery(o => o.HeaderName = "X-CSRF-TOKEN");

// Add MVC controllers so AuthController endpoints are mapped
builder.Services.AddControllers();
builder.AddAuthenticator();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#if NET10_0_OR_GREATER
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
#endif

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery(); // after auth, before mapping endpoints

// Map MVC controllers (including AuthController)
app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();