using DiscordBot.Core.Abstractions;
using DiscordBot.Core.Models.Contexts;
using DiscordBot.Core.Models.Responses;

namespace DiscordBot.Handlers;

public class EchoCommand : ICommandHandler
{
    public bool CanHandle(InteractionContext context)
    {
        return context.Command.Name == "echo";
    }

    public async Task<DiscordResponse> HandleAsync(InteractionContext context)
    {
        var response = new DiscordResponse
        {
            Type = ResponseTypes.CHANNEL_MESSAGE_WITH_SOURCE,
            Data = new ResponseData
            {
                Content = context.Command.GetParam<string>("message")
            }
        };

        return await Task.FromResult(response);
    }
}