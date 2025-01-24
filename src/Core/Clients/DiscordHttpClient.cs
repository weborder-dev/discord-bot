using Refit;

namespace DiscordBot.Core.Clients;

public interface IDiscordHttpClient
{
    [Post("/applications/{appId}/commands")]
    public Task<IApiResponse> CreateCommands(
        string appId,
        [Body(BodySerializationMethod.Serialized)] CreateCommandRequest request);
}

