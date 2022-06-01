using Microsoft.Extensions.DependencyInjection;

namespace MP.BlazorStateDemo.Tests.Infrastructure;

public class ClientHost
{
    public ClientHost(ServiceProvider aServiceProvider)
    {
        ServiceProvider = aServiceProvider;
    }

    public IServiceProvider ServiceProvider { get; }
}