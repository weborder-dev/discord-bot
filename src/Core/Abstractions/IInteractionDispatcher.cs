namespace DiscordBot.Core.Abstractions;

public interface ICommandHandlerDispatcher
{
    public Task<ICommandResult> HandleInteractionsAsync(HttpContext context);
}

public interface ICommandResult
{
    Task ExecuteAsync(HttpContext httpContext);
}
