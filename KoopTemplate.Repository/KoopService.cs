using Koop.MailKit.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace KoopTemplate.Repository;

public class KoopService
{
    private readonly IServiceProvider _serviceProvider;

    public KoopService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEmailService EmailService => _serviceProvider.GetService<IEmailService>();
}