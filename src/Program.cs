using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using DiscordBot.Core.Abstractions;
using DiscordBot.Core.Clients;
using DiscordBot.Core.Extensions;
using DiscordBot.Core.Models.Interactions;
using DiscordBot.Core.Models.Responses;
using DiscordBot.Core.Workers;
using DiscordBot.Handlers;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// 1a. Add Discrod Refit Http Client
builder.Services.AddRefitClient<IDiscordHttpClient>()
    .ConfigureHttpClient(opc =>
    {
        string botToken = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN")
            ?? string.Empty;
        opc.BaseAddress = new Uri("https://discord.com/api/v10");
        opc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", botToken);
    });


// 1b. Add worker to send request to create command when the web app starts
builder.Services.AddHostedService<CommandInitializatonWorker>();

// 1c. Add Hablders to DI Container
builder.Services.AddTransient<ICommandHandler, EchoCommand>();

var app = builder.Build();

app.MapPost("/bot", async (
    HttpContext ctx,
    ILogger<Program> Logger,
    IEnumerable<ICommandHandler> commands) =>
{
    // 2. Validate Request Signature
    bool isValid = await ctx.ValidateRequestAsync();
    if (!isValid)
    {
        return Results.BadRequest("Invlid Signature.");
    }

    // 3. Get discord interaction payload from request
    var iCtx = await ctx.CreateDiscrodInteractionContextAsync();

    // 4. Hanlde PING request.
    if (iCtx.EventType == InteractionTypes.PING)
    {
        // 4a. Return POMG
        return Results.Ok(DiscordResponse.Pong());
    }

    // 5. Loop thru all registred commands
    foreach (var cmd in commands)
    {
        // 5a. Check if command can be handled
        bool canHandle = cmd.CanHandle(iCtx);
        if (canHandle)
        {
            // 5b. Execute Command Handler
            var response = await cmd.HandleAsync(iCtx);

            // 5c. Return the response
            return Results.Ok(response);
        }
    }

    // 6. if no command handlers found return badrequest
    Logger.LogWarning("No handlers found for command {cmd}", iCtx.Command.Name);
    
    return Results.BadRequest($"No handlers found for command {iCtx.Command.Name}");
});

app.Run();
