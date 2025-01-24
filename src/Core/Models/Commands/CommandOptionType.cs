namespace DiscordBot.Core.Models.Commands;

public class CommandOptionTypes
{
    public const int SUB_COMMAND = 1;
    public const int SUB_COMMAND_GROUP = 2;
    public const int STRING = 3;
    public const int INTEGER = 4;   // Any integer between -2^53 and 2^53
    public const int BOOLEAN = 5;
    public const int USER = 6;
    public const int CHANNEL = 7;  // Includes all channel types + categories
    public const int ROLE = 8;
    public const int MENTIONABLE = 9;   // Includes users and roles
    public const int NUMBER = 10;   // Any double between -2^53 and 2^53
    public const int ATTACHMENT = 11;
}