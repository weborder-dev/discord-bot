using System.Text.Json.Serialization;

namespace DiscordBot.Core.Models.Interactions;

public class ResolvedData
{
    #region Properties
        
    [JsonPropertyName("users")]
    public string[]? Users { get; init; }
    
    [JsonPropertyName("members")]
    public string[]? Members { get; init; }
   
    [JsonPropertyName("roles")]
    public string[]? Roles { get; init; }

    [JsonPropertyName("channels")]
    public string[]? Channels { get; init; }

    [JsonPropertyName("messages")]
    public string[]? Messages { get; init; }

    [JsonPropertyName("attachments")]
    public string[]? Attachments { get; init; }

    #endregion
}