using System.Net.Http.Headers;
using System.Reflection;
using DiscordBot.Core.Abstractions;
using DiscordBot.Core.bDispatchers;
using DiscordBot.Core.Builders;
using DiscordBot.Core.Clients;
using DiscordBot.Core.Models.Commands;
using DiscordBot.Core.Workers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Refit;

namespace DiscordBot.Core.Extensions;

public static class ServiceCollectionExtensions
{
    #region Public Methods

    public static IDiscordBuilder AddDiscord(
        this IServiceCollection services)
    {
        services
            .AddRefitClient<IDiscordClient>()
            .ConfigureHttpClient(opc =>
            {
                string botToken = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN")
                    ?? string.Empty;
                opc.BaseAddress = new Uri("https://discord.com/api/v10");
                opc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", botToken);
            });

        services.AddSingleton<ICommandHandlerDispatcher, DiscordCommandDispatcher>();
        services.AddHostedService<CommandInitializatonWorker>();

        return new DiscordBuilder(services);
    }

    public static IDiscordBuilder AddSlashCommands(
        this IDiscordBuilder builder, 
        IEnumerable<SlashCommand> commands)
    {
        builder.Services.AddSingleton(commands);

        return builder;
    }

    public static IDiscordBuilder RegisterCommandsForAssembly(
        this IDiscordBuilder builder,
        Assembly assembly)
    {
        var eps = assembly.DefinedTypes
            .Where(t => t is { IsAbstract: false, IsInterface: false } && t.IsAssignableTo(typeof(ICommandHandler)))
            .Select(t => ServiceDescriptor.Transient(typeof(ICommandHandler), t));

        builder.Services.TryAddEnumerable(eps);
        return builder;
    }

    #endregion
}