using DiscordBot.Core.Models.Contexts;

namespace DiscordBot.Core.Abstractions;

public interface ICommandHandler
{
    public bool CanHandle(InteractionContext context);
    public Task<ICommandResult> HandleAsync (InteractionContext context);
}