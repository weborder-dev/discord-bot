using System.Text.Json.Serialization;

namespace DiscordBot.Core.Models.Interactions;

public class Member
{
    #region Properties

    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public User? User { get; init; }

    [JsonPropertyName("nick")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Nick { get; init; }

    [JsonPropertyName("avatar")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Avatar { get; init; }

    [JsonPropertyName("roles")]
    public string[] Roles { get; init; } = [];

    [JsonPropertyName("joined_at")]
    public DateTime JoinedAt { get; init; }

    [JsonPropertyName("premium_since")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? PremiumSince { get; init; }

    [JsonPropertyName("deaf")]
    public bool Deaf { get; init; }

    [JsonPropertyName("mute")]
    public bool Mute { get; init; }

    [JsonPropertyName("flags")]
    public int Flags { get; init; }

    [JsonPropertyName("pending")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Pending { get; init; }

    [JsonPropertyName("permissions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Permissions { get; init; }

    [JsonPropertyName("communication_disabled_until")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? CommunicationDisabledUntil { get; init; }

    [JsonPropertyName("unusual_dm_activity_until")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? UnusualDMActivityUntil { get; init; }

    #endregion
}