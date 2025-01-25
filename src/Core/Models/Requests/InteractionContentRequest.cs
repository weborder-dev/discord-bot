using System.Text.Json.Serialization;

namespace DiscordBot.Core.Models.Requests;

public class InteractionContentRequest
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}