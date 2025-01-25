using DiscordBot.Core.Abstractions;
using DiscordBot.Core.Models.Responses;

namespace DiscordBot.Core.Results;

public class SuccessResult : ICommandResult
{
    #region Public Methods

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        await Task.CompletedTask;
    }

    #endregion
}