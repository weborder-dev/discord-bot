using DiscordBot.Core.Abstractions;

namespace OniCloud.Mocha.Bots.Core.Middleware;

public class InteractionDispatcherMiddleware
{
    #region Fields

    private readonly string _interactionEndpoint;

    private readonly ICommandHandlerDispatcher _dispatcher;
    private readonly RequestDelegate _request;
    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public InteractionDispatcherMiddleware(
        RequestDelegate request,
        ILogger<InteractionDispatcherMiddleware> logger,
        ICommandHandlerDispatcher dispatcher,
        string interactionEndpoint)
    {
        _dispatcher = dispatcher;
        _interactionEndpoint = interactionEndpoint;

        _request = request ?? throw new ArgumentNullException(nameof(request));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion

    #region Public Methods

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;
        if (path != _interactionEndpoint)
        {
            await _request.Invoke(context);
            return;
        }

        _logger.LogInformation("Interaction handled for {path}", path);

        var result = await _dispatcher.HandleInteractionsAsync(context);
        await result.ExecuteAsync(context);
    }

    #endregion
}

public static class InteractionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseDiscord(
        this IApplicationBuilder builder,
        string interactionEndpoint = "/interactions")
    {
        return builder.UseMiddleware<InteractionDispatcherMiddleware>(interactionEndpoint);
    }
}