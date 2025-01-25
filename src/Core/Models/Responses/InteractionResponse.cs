using System.Text.Json.Serialization;

namespace DiscordBot.Core.Models.Responses;

public class InteractionResponse
{
    #region Properties

    [JsonPropertyOrder(1)]
    [JsonPropertyName("type")]
    public int Type { get; init; } = InteractionResponseTypes.PONG;

    [JsonPropertyOrder(2)]
    [JsonPropertyName("data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InteractionResponseData? Data { get; init; }

    #endregion

    #region Public Static Methods

    public static InteractionResponse Pong()
    {
        return new InteractionResponse
        {
            Type = InteractionResponseTypes.PONG
        };
    }

    public static InteractionResponse ReplayToChannel(string message)
    {
        return new InteractionResponse
        {
            Type = InteractionResponseTypes.CHANNEL_MESSAGE_WITH_SOURCE,
            Data = new InteractionResponseData
            {
                Content = message
            }
        };
    }

    public static InteractionResponse ReplayToChannelDefered(string message)
    {
        return new InteractionResponse
        {
            Type = InteractionResponseTypes.DEFERRED_CHANNEL_MESSAGE_WITH_SOURCE,
            Data = new InteractionResponseData
            {
                Content = message
            }
        };
    }

    public static InteractionResponse ReplayToChannelDefered()
    {
        return ReplayToChannelDefered(string.Empty);
    }

    #endregion
}
