using System.Text.Json.Serialization;

namespace DiscordBot.Core.Models.Commands;

public class CommandChoice
{
    #region Properties

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("value")]
    public object? Value { get; init; }
        
    #endregion
}