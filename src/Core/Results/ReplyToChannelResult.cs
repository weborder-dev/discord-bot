using DiscordBot.Core.Abstractions;
using DiscordBot.Core.Models.Responses;

namespace DiscordBot.Core.Results;

public class ReplyToChannelResult : ICommandResult
{
    #region Fields

    private readonly string _message;
    private readonly bool _deferred;

    #endregion

    #region Constructors

    public ReplyToChannelResult(
        string message,
        bool deferred = false)
    {
        _message = message;
        _deferred = deferred;
    }

    #endregion

    #region Public Methods

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        var payload = new InteractionResponse
        {
            Type = _deferred
                   ? InteractionResponseTypes.DEFERRED_CHANNEL_MESSAGE_WITH_SOURCE
                   : InteractionResponseTypes.CHANNEL_MESSAGE_WITH_SOURCE,

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