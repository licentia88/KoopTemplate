using KoopTemplate.RestClient.Services.Base;
using KoopTemplate.Shared.Services.Rest;

namespace KoopTemplate.RestClient.Services;

public class TestService : ITestRestService
{
    public KoopTemplateClient Client { get; set; }
    public async Task<string> GreetGetAsync(string name)
    {
        var response = await Client.SendAsync<string>(HttpMethod.Post,$"/api/test/greet?name={name}");
        return response;
    }
}
 