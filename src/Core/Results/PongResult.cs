using DiscordBot.Core.Abstractions;
using DiscordBot.Core.Models.Responses;

namespace DiscordBot.Core.Results;

public class PongResult : ICommandResult
{
    #region Public Methods

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        var response = new InteractionResponse
        {
            Type = InteractionResponseTypes.PONG
        };

        httpContext.Response.StatusCode = 200;
        await httpContext.Response.WriteAsJsonAsync(response);
    }

    #endregion
}