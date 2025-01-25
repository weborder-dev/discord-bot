
namespace DiscordBot.Core.Builders;

public class DiscordBuilder : IDiscordBuilder
{
    #region Properties

    public IServiceCollection Services { get; }

    #endregion

    #region Constructors

    public DiscordBuilder(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    #endregion
}