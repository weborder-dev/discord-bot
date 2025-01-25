using DiscordBot.Core.Abstractions;

namespace DiscordBot.Core.Results;

public class UnauthorizedResult : ICommandResult
{
    #region Public Methods

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = 200;
        return Task.CompletedTask;
    }

    #endregion
}