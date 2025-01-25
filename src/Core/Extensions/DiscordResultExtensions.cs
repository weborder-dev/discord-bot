using DiscordBot.Core.Abstractions;
using DiscordBot.Core.Results;

namespace DiscordBot.Core.Extensions;

public static class CommandResults
{
    #region Public Methods

    public static ICommandResult Success()
    {
        return new SuccessResult();
    }

    public static ICommandResult ReplyToChannel(string message)
    {
        return new ReplyToChannelResult(message);
    }

    public static ICommandResult ReplyToChannelDeferred(string message)
    {
        return new ReplyToChannelResult(message, true);
    }

    public static ICommandResult Pong()
    { 
        return new PongResult();
    }

    public static ICommandResult BadRequest(string message)
    {
        return new BadRequestResult(message);
    }
    
    public static ICommandResult Unauthorized()
    {
        return new UnauthorizedResult();
    }

    #endregion
}