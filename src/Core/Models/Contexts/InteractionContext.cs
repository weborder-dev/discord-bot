using DiscordBot.Core.Clients;
using DiscordBot.Core.Models.Interactions;
using DiscordBot.Core.Models.Requests;
using DiscordBot.Core.Models.Responses;

namespace DiscordBot.Core.Models.Contexts;

public class InteractionContext
{
    #region Fields

    private readonly IDiscordClient _client;

    #endregion

    #region Properties

    public required string Id { get; init; }
    public required InteractionTypes EventType { get; init; }
    public string ApplicationId { get; init; }
    public required string ReplyToken { get; init; }
    public required InteractionCommand Command { get; set; }
    public required User? User { get; set; }
    public required Channel? Channel { get; set; }

    #endregion

    #region Constructors

    public InteractionContext(
        string appId,
        IDiscordClient client)
    {
        _client = client;
        ApplicationId = appId;
    }

    #endregion

    #region Public Methods
    
    public async Task ReplayToChannel(string message)
    {
        var request = InteractionResponse.ReplayToChannel(message);
        await _client.ReplyToChannel(Id, ReplyToken, request);
    }

    public async Task ReplayWithFollowup(string message)
    {
        var request = new InteractionContentRequest{Content = message};
        await _client.CreateFollowupMessage(ApplicationId, ReplyToken, request);
    }
    
    public async Task ReplayToChannelDefered(string message)
    {
        var request = InteractionResponse.ReplayToChannelDefered(message);
        await _client.ReplyToChannel(Id, ReplyToken, request);
    }
   
    public async Task ReplayToChannelDefered()
    {
        await ReplayToChannelDefered(string.Empty);
    }

    #endregion
}