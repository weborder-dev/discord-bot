using System.Text.Json.Serialization;
using DiscordBot.Core.Models.Commands;

namespace DiscordBot.Core.Models.Interactions;

public class DataOption
{
    #region Properteis

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public CommandOptionTypes Type { get; init; }

    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Value { get; init; }

    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DataOption[]? Options { get; init; }

    [JsonPropertyName("focused")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Focused { get; init; }

    #endregion
}