using System.Text.Json.Serialization;

namespace DiscordBot.Core.Models.Interactions;

public class InteractionData
{
    #region Properties

    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    ///     CHAT_INPUT = 1,     // Slash commands; a text-based command that shows up when a user types /
    ///     USER = 2,           // A UI-based command that shows up when you right click or tap on a user
    ///     MESSAGE = 3         // A UI-based command that shows up when you right click or tap on a message
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; init; }

    [JsonPropertyName("resolved")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ResolvedData? Resolved { get; init; }

    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DataOption[]? Options { get; init; }

    [JsonPropertyName("guild_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GuildId { get; init; }

    [JsonPropertyName("target_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TargetId { get; init; }

    #endregion
}