
using System.Text.Json;
using DiscordBot.Core.Clients;
using DiscordBot.Core.Models.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBot.Core.Workers;

public class CommandInitializatonWorker : BackgroundService
{
    #region Fields

    private readonly IDiscordHttpClient _client;
    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public CommandInitializatonWorker(
        IDiscordHttpClient client,
        ILogger<CommandInitializatonWorker> logger)
    {
        _client = client;
        _logger = logger;
    }

    #endregion

    #region Private/Protected Methods

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var appId = Environment.GetEnvironmentVariable("DISCORD_APP_ID") ?? string.Empty;
        var payload = BuildCreateCommand();

        var response = await _client.CreateCommands(appId, payload);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning(
                "Unable to create/upsert command {err}",
                response.Error.Content);
        }
    }

    protected CreateCommandRequest BuildCreateCommand()
    {
        var request =  new CreateCommandRequest
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
        };

        _logger.LogInformation("{req}", JsonSerializer.Serialize(request));

        return request;
    }

    #endregion
}