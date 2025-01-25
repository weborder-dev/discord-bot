using System.Text.Json.Serialization;
using DiscordBot.Core.Models.Commands;
using DiscordBot.Core.Models.Requests;
using DiscordBot.Core.Models.Responses;
using Refit;

namespace DiscordBot.Core.Clients;

public interface IDiscordClient
{
    [Post("/applications/{appId}/commands")]
    public Task<IApiResponse> CreateCommands(
        string appId,
        SlashCommand payload);

    [Post("/interactions/{id}/{token}/callback")]
    public Task<IApiResponse> ReplyToChannel(
        string id,
        string token,
        InteractionResponse payload);

    [Post("/webhooks/{appId}/{interactionToken}")]
    public Task<IApiResponse> CreateFollowupMessage(
        string appId, 
        string interactionToken,
        InteractionContentRequest payload);
}
