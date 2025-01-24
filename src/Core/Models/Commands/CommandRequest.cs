using System.Text.Json.Serialization;

namespace DiscordBot.Core.Models.Commands;

public class CommandRequest
{
    #region Properties

    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public int Type { get; init; }

    [JsonPropertyName("application_id")]
    public string ApplicationId { get; init; } = string.Empty;

    [JsonPropertyName("guild_id")]
    public string? GuildId { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<CommandOption>? Options { get; init; }

    [JsonPropertyName("default_member_permissions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DefaultMemberPermissions { get; init; }

    [JsonPropertyName("dm_permission")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DmPermissions { get; init; }

    [JsonPropertyName("nsfw")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsAgeRestricted { get; init; }

    [JsonPropertyName("version")]
    public string Version { get; init; } = string.Empty;

    #endregion
}
