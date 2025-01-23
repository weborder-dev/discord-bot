using System.Text.Json.Serialization;

namespace DiscordBot.Core.Models.Responses;

public class DiscordResponse
{
    #region Properties

    [JsonPropertyOrder(1)]
    [JsonPropertyName("type")]
    public ResponseTypes Type { get; init; } = ResponseTypes.PONG;

    [JsonPropertyOrder(2)]
    [JsonPropertyName("data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ResponseData? Data { get; init; }

    #endregion

    #region Public Static Methods

    public static DiscordResponse Pong()
    {
        return new DiscordResponse
        {
            Type = ResponseTypes.PONG
        };
    }

    #endregion
}
