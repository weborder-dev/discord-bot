using DiscordBot.Core.Clients;
using DiscordBot.Core.Models.Commands;

namespace DiscordBot.Core.Workers;

public class CommandInitializatonWorker : BackgroundService
{
    #region Fields

    private readonly IDiscordClient _client;
    private readonly IEnumerable<SlashCommand> _commands;
    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public CommandInitializatonWorker(
        IEnumerable<SlashCommand> commands,
        IDiscordClient client,
        ILogger<CommandInitializatonWorker> logger)
    {
        _commands = commands;
        _client = client;
        _logger = logger;
    }

    #endregion

    #region Private/Protected Methods

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var appId = Environment.GetEnvironmentVariable("DISCORD_APP_ID") ?? string.Empty;

        foreach (var cmd in _commands)
        {
            var response = await _client.CreateCommands(appId, cmd);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Unable to create/upsert command {err}",
                    response.Error.Content);
            }
            
            _logger.LogInformation("Slash command {cn} created/updated", cmd.Name);
        }
    }

    #endregion
}