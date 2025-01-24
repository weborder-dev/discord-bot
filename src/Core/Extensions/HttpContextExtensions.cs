using System.Text.Json;
using DiscordBot.Core.Models.Contexts;
using DiscordBot.Core.Models.Requests;
using NSec.Cryptography;

namespace DiscordBot.Core.Extensions;

public static class HttpContextExtensions
{
    #region Public Methods

    public static async Task<bool> ValidateRequestAsync(this HttpContext context)
    {
        string publicKey = Environment.GetEnvironmentVariable("DISCORD_APP_PUBLIC_KEY")
            ?? string.Empty;

        context.Request.EnableBuffering();
        using StreamReader reader = new(context.Request.Body, leaveOpen: true);
        string payload = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        bool isRequestValid = context.ValidateEd25519Signature(payload, publicKey);

        return isRequestValid;
    }

    public static async Task<InteractionContext> CreateDiscrodInteractionContextAsync(this HttpContext context)
    {
        context.Request.EnableBuffering();
        using StreamReader reader = new(context.Request.Body, leaveOpen: true);
        string payload = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        var interaction = JsonSerializer.Deserialize<InteractionRequest>(payload)
            ?? throw new InvalidOperationException("Error in payload");

        return new InteractionContext
        {
            EventType = interaction.Type,
            ReplyToken = interaction.Token,
            ReplyUrl = GetReplyUrl(interaction.Token),
            Channel = interaction.Channel,
            User = interaction.User,
            Command = InteractionCommand.CreateCommand(interaction)
        };
    }

    #endregion

    #region Private Methods

    private static bool ValidateEd25519Signature(
        this HttpContext context,
        string payload,
        string publicKey,
        string SignatureHeaderName = "X-Signature-Ed25519",
        string SignatureTimesampHeaderName = "X-Signature-Timestamp")
    {
        string? signature = context.Request.Headers[SignatureHeaderName].ToString();
        string? timestamp = context.Request.Headers[SignatureTimesampHeaderName].ToString();

        string hmac = GetHash(signature);

        bool isValid = ValidateHash(
            $"{timestamp}{payload}",
            publicKey,
            hmac);

        return isValid;
    }

    private static bool ValidateHash(string message, string publicKey, string hash)
    {
        ReadOnlySpan<byte> data = message.ToByteSpan();
        ReadOnlySpan<byte> pKey = publicKey.HexStringToByteSpan();
        ReadOnlySpan<byte> sig = hash.HexStringToByteSpan();
        Ed25519 alg = SignatureAlgorithm.Ed25519;
        PublicKey pk = PublicKey.Import(alg, pKey, KeyBlobFormat.RawPublicKey);
        
        bool isValid = alg.Verify(pk, data, sig);
        
        return isValid;
    }

    private static string GetHash(string hash)
    {
        if (hash.StartsWith("ed25519="))
        {
            return hash[9..];
        }
        return hash;
    }

    private static string GetReplyUrl(string token)
    {
        string discordUrl = Environment.GetEnvironmentVariable("DISCORD_API_URL")
            ?? "https://discord.com/api/v10";

        string applicationId = Environment.GetEnvironmentVariable("DISCORD_APPID")
            ?? string.Empty;

        return $"{discordUrl}/webhooks/{applicationId}/{token}/messages/@original";
    }

    #endregion
}