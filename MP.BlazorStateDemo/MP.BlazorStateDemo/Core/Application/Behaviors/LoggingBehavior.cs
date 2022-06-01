using MediatR;

namespace MP.BlazorStateDemo.Core.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Serilog.ILogger _logger;

    public LoggingBehavior(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        _logger
            .Information($"Handling {typeof(TRequest).Name}");

        var response = await next()
            .ConfigureAwait(false);

        _logger
            .Information($"Handled {typeof(TRequest).Name}");

        return response;
    }
}