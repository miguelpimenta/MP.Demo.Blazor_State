using BlazorState;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MP.BlazorStateDemo.Core.Application.Behaviors;
using Serilog;
using System.Reflection;

namespace MP.BlazorStateDemo.IntegrationTests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .CreateLogger();

        services
            .AddSingleton(Log.Logger);

        services
            .AddMediatR(typeof(Program));

        //! Mediatr Behaviors
        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        //! Blazor-State
        var assembly = typeof(Program)
            .GetTypeInfo().Assembly;

        services
            .AddBlazorState
            (
                (opt) => opt.Assemblies = new Assembly[] { assembly }
            );
    }
}