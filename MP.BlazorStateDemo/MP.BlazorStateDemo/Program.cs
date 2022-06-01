using BlazorState;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MP.BlazorStateDemo;
using MP.BlazorStateDemo.Core.Application.Behaviors;
using MudBlazor.Services;
using Serilog;
using System.Reflection;

var builder = WebAssemblyHostBuilder
    .CreateDefault(args);

builder.RootComponents
    .Add<App>("#app");
builder.RootComponents
    .Add<HeadOutlet>("head::after");

var config = builder.Configuration;

Log.Logger = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Debug()
    .WriteTo.BrowserConsole()
#else
    //...
#endif
    .CreateLogger();

builder.Services
    .AddSingleton(Log.Logger);

#if DEBUG

Log.Debug("Browser Log Test!");

#endif

builder.Services
    .AddScoped(sp => new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

//! MudBlazor
builder.Services
    .AddMudServices();

//! SQLite
//builder.Services
//    .AddSqliteWasmDbContextFactory<AppDbContext>(opts =>
//{
//    opts.UseSqlite("Data Source=database.db");
//    opts.UseSnakeCaseNamingConvention();
//});

//! Mediatr
builder.Services
    .AddMediatR(typeof(Program));

//! Mediatr Behaviors
builder.Services
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

//! Blazor-State
var assembly = typeof(Program).GetTypeInfo().Assembly;

builder.Services
    .AddBlazorState
    (
        (opt) => opt.Assemblies = new Assembly[] { assembly }
    );

var host = builder
    .Build()
    .RunAsync();

public partial class Program
{ }