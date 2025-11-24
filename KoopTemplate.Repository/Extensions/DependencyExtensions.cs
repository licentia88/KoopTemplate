using Koop.MailKit.Contracts;
using Koop.MailKit.Infrastructures.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KoopTemplate.Repository.Extensions;

public static class DependencyExtensions
{
    public static IServiceCollection RegisterRepository(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        
        
        services.AddScoped<KoopService>();
        
        return services;
    }
}