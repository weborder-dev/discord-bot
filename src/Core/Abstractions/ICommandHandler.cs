using DiscordBot.Core.Models.Contexts;
using DiscordBot.Core.Models.Responses;

namespace DiscordBot.Core.Abstractions;

public interface ICommandHandler
{
    public bool CanHandle(InteractionContext context);
    public Task<DiscordResponse> HandleAsync (InteractionContext context);
}