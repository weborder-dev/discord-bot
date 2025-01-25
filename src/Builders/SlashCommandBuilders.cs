using DiscordBot.Core.Models.Commands;

namespace DiscordBot.Builders;

public static class SlashCommandBuilders
{
    public static IEnumerable<SlashCommand> BuildCommands()
    {
        return new List<SlashCommand>
        {
            new SlashCommand
            {
                Name = "echo",
                Description = "This is a sample echo command",
                Options =
                [
                    new()
                    {
                        Type = CommandOptionTypes.STRING,
                        Name = "message",
                        Description = "The message to echo back",
                        Required = true
                    }
                ]
            }
        };
    }
}