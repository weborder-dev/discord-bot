namespace DiscordBot.Core.Models.Responses;

public enum ResponseTypes
{
    /// <summary>
    /// ACK a Ping
    /// <summary>
    PONG = 1, // 

    /// <summary>
    /// respond to an interaction with a message
    /// </summary>
    CHANNEL_MESSAGE_WITH_SOURCE = 4,

    /// <summary>
    /// ACK an interaction and edit a response later, the user sees a loading state
    /// </summary>
    DEFERRED_CHANNEL_MESSAGE_WITH_SOURCE = 5,

    /// <summary>
    /// for components, ACK an interaction and edit the original message later, the user does not see a loading state
    /// </summary>
    DEFERRED_UPDATE_MESSAGE = 6,

    /// <summary>
    /// for components, edit the message the component was attached to
    /// </summary>
    UPDATE_MESSAGE = 7,

    /// <summary>
    /// respond to an autocomplete interaction with suggested choices
    /// </summary>
    APPLICATION_COMMAND_AUTOCOMPLETE_RESULT = 8,

    /// <summary>
    /// respond to an interaction with a popup modal
    /// </summary>
    MODAL = 9
}
