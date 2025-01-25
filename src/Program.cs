using System.Reflection;
using DiscordBot.Builders;
using DiscordBot.Core.Extensions;
using OniCloud.Mocha.Bots.Core.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add Discord Interactions SDK Required Services, Add Command Definitons and Register Command Handlers
builder.Services
    .AddDiscord()
    .AddSlashCommands(SlashCommandBuilders.BuildCommands())
    .RegisterCommandsForAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseDiscord("/bot");

app.Run();
