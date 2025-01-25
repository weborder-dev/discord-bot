# Creating a Discrod Slash Command Application


## Create a Discord Applicaton: A Step-by-Step Guide

Creating a Discord bot is an exciting way to add custom functionality to your Discord server. This tutorial focuses on the steps you need to take in the Discord Developer Portal (Application Dashboard) to set up your bot.

### Step 1: Log in to the Discord Developer Portal

1. Open your browser and go to [Discord Developer Portal](https://discord.com/developers/applications).
2. Log in using your Discord credentials if you aren’t already signed in.
![discord app portal](/imgs/app-portal.png)

### Step 2: Create a New Application

1. Once logged in, click on the **"New Application"** button in the top-right corner.
2. In the pop-up window, enter a name for your application (e.g., "MyFirstApp") and click **"Create"**.
3. Add a description and tags.
4. In the **"Interactions Endpoint URL"** textbox add the url of your interactions hanlder web app.
![interactions endpoint](/imgs/interactions-endpoint.png)

    **Important:** Writ down the application id and public key. --you’ll need it when coding your bot.
    ![app id and public key](/imgs/app-id-key.png)

### Step 3: Set Up the Bot in Your Application

1. On your application’s dashboard, navigate to the **"Bot"** tab in the left-hand menu.
2. Click the **"Add Bot"** button and confirm by clicking **"Yes, do it!"** when prompted.
3. Your bot is now created! You’ll see its username and avatar (default for now).

### Step 4: Customize Your Bot

1. **Name**: You can rename your bot by editing its username under the bot settings.
2. **Avatar**: Upload an avatar image to personalize your bot.
3. **Token**: This token is essential for connecting your bot to the Discord API. Click **"Reset Token"** to generate a new token if needed. Make sure to **copy and save your token** securely – you’ll need it when coding your bot.

   **Important:** Never share your token publicly or commit it to version control systems like GitHub. If it is exposed, reset it immediately.

### Step 5: Set Permissions for the Bot

1. In the **"Bot"** tab, scroll down to the **"Privileged Gateway Intents"** section. Enable any intents that your bot needs (e.g., "Presence Intent" or "Server Members Intent") based on its functionality.
2. Save your changes.

### Step 6: Add Your Bot to a Discord Server

1. Navigate to the **"OAuth2"** tab in the left-hand menu.
2. Under **"OAuth2 URL Generator"**:
   - Select the **"bot"** scope.
   - In the **"Bot Permissions"** section, choose the permissions your bot will require. For example, if your bot will manage roles, select the "Manage Roles" permission.
3. Copy the generated URL at the bottom of the page.
4. Paste the URL into your browser, select the server where you want to add the bot, and click **"Authorize"**. Ensure you have "Manage Server" permissions on the server you choose.

### Step 7: Test Your Bot

Your bot is now added to your server! While it’s not functional yet, this completes the setup in the Discord Developer Portal. Next, you’ll need to write code to give your bot its functionality. You can use libraries like [discord.js](https://discord.js.org/) for JavaScript or [discord.py](https://discordpy.readthedocs.io/) for Python.

# Craete the Dicord Interaction Handler
### Step 0: Prerequisites
To run the Discord interaction hanlder app securely, you need to configure the following environment variables. These variables store sensitive information and application details required to connect your bot/app to the Discord API.

1. DISCORD_BOT_TOKEN:
    * **Description:** This is the bot token used to authenticate your bot with Discord’s API.
    * **Where to find:** You can obtain the token from the Bot tab in the Discord Developer Portal for your application.
        * Navigate to the Developer Portal.
        * Select your application.
        * Go to the Bot tab.
        * Copy the token securely (do not share it).

2. DISCORD_APP_ID
    * **Description:** This is your application ID, a unique identifier for your bot application.
    * **Where to find:** You can find this ID on your application’s main page in the Discord Developer Portal.

3. DISCORD_APP_PUBLIC_KEY
    * **Description:** This is the public key associated with your application.
    * **Where to find:** Go to the General Information section of your application in the Discord Developer Portal and copy the Public Key.

#### **Setup variables in your shell**
Open your terminal and create the environment variables.
```bash
export DISCORD_BOT_TOKEN=<your-bot-token>
export DISCORD_APP_ID=<your-application-id>
export DISCORD_APP_PUBLIC_KEY=<your-public-key>
```

### Step 1: Create a C# web application
``` bash
dotnet new web -n DiscordSlash -o ./
```

### Step 2: Add the following Nuget Package
``` bash
dotnet add package OniCloud.Mocha.Discord
```

### Step 3: Create the Command Definition
In order to create a slash command, we need to model the command using the `SlashCommand` class. This process involves defining the command structure, which includes its name, description, and any options it might take. This command will be created/updated every time the application starts.

The following class creates the slash command definition for an `echo` command:
```cs
public static class SlashCommandBuilders
{
    public static IEnumerable<SlashCommand> BuildCommands()
    {
        return new List<SlashCommand>
        {
            new SlashCommand
            {
                Name = "echo",
                Description = "This is a sample echo command",
                Options =
                [
                    new()
                    {
                        Type = CommandOptionTypes.STRING,
                        Name = "message",
                        Description = "The message to echo back",
                        Required = true
                    }
                ]
            }
        };
    }
}
```

### Step 4: Create a the slash command handler class.
The `ICommandHandler` interface is designed for creating handlers for slash commands. By implementing this interface in multiple classes, you can assign each class to handle specific commands and their subcommands. This structure helps organize your code efficiently, making it more maintainable and scalable. Each class is dedicated to managing a particular request or functionality, ensuring modularity and focus.

For instance, if you're building a slash command system with commands like "OrderPizza" and "TrackOrder," you could create separate classes such as OrderPizzaHandler and TrackOrderHandler. Each class would implement its own canHandle and handle methods, processing only the requests relevant to its assigned functionality. This separation of concerns enhances code readability, simplifies debugging, and allows you to add new commands or subcommands seamlessly without disrupting existing features.

The following example demonstrates a class that implements the command handler for the "echo" slash command defined in the previous step.

```cs
public class EchoCommand : ICommandHandler
{
    public bool CanHandle(InteractionContext context)
    {
        return context.Command.Name == "echo";
    }

    public async Task<ICommandResult> HandleAsync(InteractionContext context)
    {
        var msg = context.Command.GetParam<string>("message") ?? "No message provided";
        var result = CommandResults.ReplyToChannel(msg);

        return await Task.FromResult(result);
    }
}
```

The `CanHandle` method in an request handler determines whether the handler should process the current request. It acts as a filter, ensuring that only relevant requests are handled by a specific handler.

The `Handle` method is where you define the logic for responding to a user's slash command request. It is part of a request handler, which processes specific intents or requests.

By structuring your code this way, you can efficiently manage complex applications with multiple features while keeping the implementation organized and scalable.
 
### Step 5: Register required services, command definitions and command Handlers
Once you finish building your Command Hanlders classes you need to register them in the DI container as Transient. In your `Program.cs` write the following code:

```cs
...
// Add Discord Interactions SDK Required Services, Add Command Definitons and Register Command Handlers
builder.Services
    .AddDiscord()
    .AddSlashCommands(SlashCommandBuilders.BuildCommands())
    .RegisterCommandsForAssembly(Assembly.GetExecutingAssembly());
...
```

### Step 6: Add Middleware Handler
When a user interacts with your application through Discord's user interface, Discord sends a POST request to the configurated discord application interactions endpoint, in our case it will be https://<url>/interactions. To handle such requests we need to call the Discord interactions handler middleware. 

```cs
...
app.UseDiscord("/interactions");
...
```

The `app.UseDiscord("/interactions")` middleware call configures the application to handle requests sent to the `/interactions` endpoint by Discord. This endpoint is the responsable for managing the slash command interaction handlers. 

The complete code for program.cs
```cs
var builder = WebApplication.CreateBuilder(args);

// Add Discord Interactions SDK Required Services, Add Command Definitons and Register Command Handlers
builder.Services
    .AddDiscord()
    .AddSlashCommands(SlashCommandBuilders.BuildCommands())
    .RegisterCommandsForAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseDiscord("/interactions");

app.Run();
```