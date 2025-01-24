namespace DiscordBot.Core.Models.Commands;

public class CommandTypes
{
    public const int CHAT_INPUT = 1;    // Slash commands; a text-based command that shows up when a user types /
    public const int USER = 2;          // A UI-based command that shows up when you right click or tap on a user
public const int MESSAGE = 3;           // A UI-based command that shows up when you right click or tap on a message
}