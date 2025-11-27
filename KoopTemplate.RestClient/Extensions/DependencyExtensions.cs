using Koop.Rest;
using Koop.Rest.Infrastructures.Services;
using KoopTemplate.RestClient.Services;
using KoopTemplate.RestClient.Services.Base;
using KoopTemplate.Shared.Services.Rest;
using Microsoft.Extensions.DependencyInjection;

namespace KoopTemplate.RestClient.Extensions;

public static class DependencyExtensions
{
    public static IServiceCollection RegisterKoopTemplateClient(IServiceCollection serviceCollection)
    {
       

// // Basit GET
//         var dto = await client.SendAsync<MyDto>(HttpMethod.Get, "/items/42");
//
// // Model ile GET -> Query string otomatik
//         var req = new ItemQuery { PageSize = 50, Active = true };
//         var response = await client.SendAsync<ItemResponse, ItemQuery>(HttpMethod.Get, "/items", req);
//
// // POST (JSON body)
//         var create = new ItemCreate { Name = "Test" };
//         var created = await client.SendAsync<ItemResponse, ItemCreate>(HttpMethod.Post, "/items", create);
        serviceCollection.AddScoped(x =>
        {
            var client = ClientBase.Create(handler => new KoopTemplateClient(handler), new Uri("https://localhost:7126/"));
            return client;
        });

        return serviceCollection;
    }
}