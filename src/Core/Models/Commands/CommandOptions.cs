using System.Text.Json.Serialization;

namespace DiscordBot.Core.Models.Commands;

public class CommandOption
{
    #region Properties

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Type { get; set; }

    [JsonPropertyName("choices")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<CommandChoice>? Choices { get; set; } = [];

    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<CommandOption>? Options { get; set; }

    [JsonPropertyName("required")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Required { get; set; }
    
    [JsonPropertyName("channel_types")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int[]? ChannelTypes { get; set; }
    
    [JsonPropertyName("min_value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MinValue { get; set; }
    
    [JsonPropertyName("max_value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MaxValue { get; set; }

    [JsonPropertyName("min_length")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MinLength { get; set; }
    
    [JsonPropertyName("max_length")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MaxLength { get; set; }
    
    [JsonPropertyName("autocomplete")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Autocomplete { get; set; }

    #endregion
}