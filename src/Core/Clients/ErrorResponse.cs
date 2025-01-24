using System.Text.Json.Serialization;

namespace DiscordBot.Core.Clients;

public class DiscordHttpClientErrorResponse
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("errors")]
    public DiscordHttpClientErrorList? Errors { get; set; }
}

public class DiscordHttpClientErrorList
{
    [JsonPropertyName("_errors")]
    public List<DiscordHttpClientError> Errors { get; set; } = [];
}

public class DiscordHttpClientError
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
