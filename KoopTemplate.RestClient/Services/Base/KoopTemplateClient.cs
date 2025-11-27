using Koop.Rest.Infrastructures.Services;

namespace KoopTemplate.RestClient.Services.Base;

 
public class KoopTemplateClient:KoopClient
{
    public KoopTemplateClient() : base()
    {
    }

 

    public KoopTemplateClient(HttpMessageHandler handler) : base(handler)
    {
    }
}