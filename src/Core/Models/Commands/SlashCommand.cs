using System.Text.Json.Serialization;

namespace DiscordBot.Core.Models.Commands;

public class SlashCommand
{
    #region Properties

    [JsonPropertyName("type")]
    public int Type => CommandTypes.CHAT_INPUT;

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("options")]
    public List<CommandOption> Options { get; init; } = [];

    #endregion
}