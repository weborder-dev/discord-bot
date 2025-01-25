using DiscordBot.Core.Abstractions;
using DiscordBot.Core.Extensions;
using DiscordBot.Core.Models.Contexts;

namespace DiscordBot.Handlers;

public class EchoCommand : ICommandHandler
{
    public bool CanHandle(InteractionContext context)
    {
        return context.Command.Name == "echo";
    }

    public async Task<ICommandResult> HandleAsync(InteractionContext context)
    {
        var msg = context.Command.GetParam<string>("message") ?? "No message provided";
        var defer = context.Command.GetParam<bool>("defer");
        
        if (!defer)
        {
            return CommandResults.ReplyToChannel(msg);
        }

        await context.ReplayToChannelDefered();
        await Task.Delay(5000);
        await context.ReplayWithFollowup(msg);

        return  CommandResults.Success();
    }
}