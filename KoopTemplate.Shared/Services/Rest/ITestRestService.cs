namespace KoopTemplate.Shared.Services.Rest;

public interface ITestRestService
{
    public Task<string> GreetGetAsync(string name);
}