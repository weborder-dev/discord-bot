using System.Text.Json.Serialization;

namespace DiscordBot.Core.Models.Responses;

public class InteractionResponseData
{
    #region Properties

    [JsonPropertyName("tts")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Tts { get; init; }

    [JsonPropertyName("content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Content { get; init; }

    #endregion
}