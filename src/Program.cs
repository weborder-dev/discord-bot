using DiscordBot.Core.Abstractions;
using DiscordBot.Core.Extensions;
using DiscordBot.Core.Models.Interactions;
using DiscordBot.Core.Models.Responses;
using DiscordBot.Handlers;

var builder = WebApplication.CreateBuilder(args);

// 0. Add Hablders to DI Container
builder.Services.AddTransient<ICommandHandler, EchoCommand>();

var app = builder.Build();

app.MapPost("/bot", async (
    HttpContext ctx,
    IEnumerable<ICommandHandler> commands) =>
{
    // 1. Validate Request Signature
    bool isValid = await ctx.ValidateRequestAsync();
    if (!isValid)
    {
        return Results.BadRequest("Invlid Signature.");
    }

    // 2. Get discord interaction payload from request
    var iCtx = await ctx.CreateDiscrodInteractionContextAsync();

    // 3. Hanlde PING request.
    if (iCtx.EventType == InteractionTypes.PING)
    {
        // 3a. Return POMG
        return Results.Ok(DiscordResponse.Pong());
    }

    // 4. Loop thru all registred commands
    foreach (var cmd in commands)
    {
        // 4a. Check if command can be handled
        bool canHandle = cmd.CanHandle(iCtx);
        if (canHandle)
        {
            // 4b. Execute Command Handler
            var response = await cmd.HandleAsync(iCtx);

            // 4c. Return the response
            return Results.Ok(response);
        }
    }

    // 5. if no command handlers found return badrequest
    return Results.BadRequest($"No handlers found for command {iCtx.Command.Name}");
});

app.Run();
