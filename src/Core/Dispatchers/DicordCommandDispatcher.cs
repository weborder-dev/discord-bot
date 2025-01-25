using DiscordBot.Core.Abstractions;
using DiscordBot.Core.Extensions;
using DiscordBot.Core.Models.Interactions;

namespace DiscordBot.Core.bDispatchers;

public class DiscordCommandDispatcher : ICommandHandlerDispatcher
{
    #region Fields

    private readonly IEnumerable<ICommandHandler> _handlers;
    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public DiscordCommandDispatcher(
        IEnumerable<ICommandHandler> handlers,
        ILogger<DiscordCommandDispatcher> logger)
    {
        _handlers = handlers;
        _logger = logger;
    }

    #endregion

    #region Public Methods

    public async Task<ICommandResult> HandleInteractionsAsync(HttpContext ctx)
    {
        // 2. Validate Request Signature
        bool isValid = await ctx.ValidateRequestAsync();
        if (!isValid)
        {
            return CommandResults.Unauthorized();
        }

        // 3. Get discord interaction payload from request
        var iCtx = await ctx.CreateDiscrodInteractionContextAsync();

        // 4. Hanlde PING request.
        if (iCtx.EventType == InteractionTypes.PING)
        {
            // 4a. Return POMG
            return CommandResults.Pong();
        }

        // 5. Loop thru all registred commands
        foreach (var cmd in _handlers)
        {
            // 5a. Check if command can be handled
            bool canHandle = cmd.CanHandle(iCtx);
            if (canHandle)
            {
                // 5b. Execute Command Handler
                var response = await cmd.HandleAsync(iCtx);

                // 5c. Return the response
                return response;
            }
        }

        // 6. if no command handlers found return badrequest
        _logger.LogWarning("No handlers found for command {cmd}", iCtx.Command.Name);

        return CommandResults.BadRequest("No handlers found for command {iCtx.Command.Name}");
    }

    #endregion
}
