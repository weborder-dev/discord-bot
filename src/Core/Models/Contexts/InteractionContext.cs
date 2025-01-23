using DiscordBot.Core.Models.Interactions;

namespace DiscordBot.Core.Models.Contexts;

public class InteractionContext
{
    #region Properties

    public required InteractionTypes EventType { get; init; }
    public required string ReplyToken { get; init; }
    public required string ReplyUrl { get; init; }
    public required InteractionCommand Command { get; set; }
    public required User? User { get; set; }
    public required Channel? Channel { get; set; }

    #endregion
}