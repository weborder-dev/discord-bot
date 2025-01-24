using System.Text.Json.Serialization;
using DiscordBot.Core.Models.Commands;

namespace DiscordBot.Core.Clients;

public class CreateCommandRequest
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