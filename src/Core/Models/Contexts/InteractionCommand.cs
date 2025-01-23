using DiscordBot.Core.Models.Commands;
using DiscordBot.Core.Models.Interactions;
using DiscordBot.Core.Models.Requests;

namespace DiscordBot.Core.Models.Contexts;

public class InteractionCommand
{
    #region Properties

    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Group { get; init; }
    public required string SubCommand { get; init; }
    public required Dictionary<string, object?> Params { get; init; }

    #endregion

    #region Constructors

    private InteractionCommand() { }

    #endregion

    #region Public Methods

    public static InteractionCommand InvalidCommand()
    {
        return new InteractionCommand
        {
            Id = string.Empty,
            Name = "Invalid",
            Group = string.Empty,
            SubCommand = string.Empty,
            Params = []
        };
    }

    public static InteractionCommand CreateCommand(InteractionRequest interaction)
    {
        var data = interaction.Data;
        var invalidCommand = InvalidCommand();

        if (data is null || data.Options is null)
        {
            return invalidCommand;
        }

        string group = string.Empty;
        string subCommand = string.Empty;
        Dictionary<string, object?> commandParams = [];

        foreach (var option in data.Options)
        {
            // check if we have a subcommand group
            if (option.Type == CommandOptionTypes.SUB_COMMAND_GROUP)
            {
                // Error you cannot have a subcommand group without commands
                if (option.Options is null)
                {
                    return invalidCommand;
                }

                group = option.Name;
                foreach (var op in option.Options)
                {
                    if (op.Type != CommandOptionTypes.SUB_COMMAND)
                    {
                        // Error cannot have a subcommand group without a sub command
                        return invalidCommand;
                    }

                    // we have a sub command
                    subCommand = op.Name;

                    // check if we have parameters
                    if (op.Options is not null)
                    {
                        commandParams = GetCommandParams(op.Options);
                    }
                }
            }
            else if (option.Type == CommandOptionTypes.SUB_COMMAND)
            {
                // we have a sub command
                subCommand = option.Name;

                // check if we have parameters
                if (option.Options is not null)
                {
                    commandParams = GetCommandParams(option.Options);
                }
            }
            else
            {
                // we have args for the top level.
                commandParams.Add(option.Name, Getvalue(option));
            }
        }

        return new InteractionCommand
        {
            Id = data.Id,
            Name = data.Name,
            Group = group,
            SubCommand = subCommand,
            Params = commandParams
        };
    }

    #endregion

    #region Private Methods

    private static Dictionary<string, object?> GetCommandParams(DataOption[] dataOption)
    {
        var p = new Dictionary<string, object?>();

        foreach (var args in dataOption)
        {
            p.Add(args.Name, Getvalue(args));
        }

        return p;
    }

    private static object? Getvalue(DataOption option)
    {
        string? value = Convert.ToString(option.Value);
        return option.Type switch
        {
            CommandOptionTypes.STRING => value,
            CommandOptionTypes.INTEGER => Convert.ToInt32(value),
            CommandOptionTypes.BOOLEAN => Convert.ToBoolean(value),
            _ => option.Value
        };
    }

    #endregion
}