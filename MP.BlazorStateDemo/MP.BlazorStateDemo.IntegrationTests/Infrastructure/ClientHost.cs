using Microsoft.Extensions.DependencyInjection;
using System;

namespace MP.BlazorStateDemo.IntegrationTests.Infrastructure;

[NotTest]
public class ClientHost
{

    public ClientHost(ServiceProvider aServiceProvider)
    {
        ServiceProvider = aServiceProvider;
    }

    public IServiceProvider ServiceProvider { get; }
}
