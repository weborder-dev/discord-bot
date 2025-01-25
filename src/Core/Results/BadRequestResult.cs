using DiscordBot.Core.Abstractions;
using DiscordBot.Core.Models.Responses;

namespace DiscordBot.Core.Results;

public class BadRequestResult : ICommandResult
{
    #region Fields

    private readonly string _message;

    #endregion

    #region Constructors

    public BadRequestResult(string message)
    {
        _message = message;
    }

    #endregion

    #region Public Methods

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        var payload = new InteractionResponse
        {
            Type = InteractionResponseTypes.CHANNEL_MESSAGE_WITH_SOURCE,
            Data = new InteractionResponseData
            {
                Content = _message
            }
        };

        httpContext.Response.StatusCode = 200;
        await httpContext.Response.WriteAsJsonAsync(payload);
    }

    #endregion
}