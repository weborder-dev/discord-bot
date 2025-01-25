using DiscordBot.Core.Models.Commands;

namespace DiscordBot.Builders;

public static class SlashCommandBuilders
{
    public static IEnumerable<SlashCommand> BuildCommands()
    {
        return [
            new()
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
                    },
                    new()
                    {
                        Type = CommandOptionTypes.BOOLEAN,
                        Name = "defer",
                        Description = "True to send a defer message"
                    }
                ]
            }
        ];
    }
}