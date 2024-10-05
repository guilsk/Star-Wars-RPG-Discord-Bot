# Star Wars RPG Discord Bot
This project is a Discord bot designed for managing RPG characters from the Star Wars universe. It includes functionalities for handling character sheets, storing data, and retrieving information through JSON files.

## Features
- Character management for Star Wars RPG
- JSON-based storage for saving character data
- Modular architecture for easy expansion
## Installation
1. Clone the repository:
```
git clone https://github.com/guilsk/Star-Wars-RPG-Discord-Bot.git
```
2. Open the solution in Visual Studio.
3. Restore dependencies and follow the **Usage** steps.
## Usage
1. Configure the Bot Token:
- Open the file `Program.cs` inside the `ConsoleApp` project.
- Replace the placeholder with your Discord bot token:
```
string jsonFilePath = "YOUR_DISCORD_BOT_TOKEN";
```
- To obtain a token, create a bot on the [Discord Developer Portal](https://discord.com/developers/docs/quick-start/getting-started).
2. Set the JSON File Path:
- In the `DataContext.cs` file located in `Infra/Shared`, you can modify the path where the character sheets are saved by changing the value of `NOME_ARQUIVO`:
```
private const string NOME_ARQUIVO = "path/to/your/ficha.json";
```
After setting up the bot token and adjusting the path, build and run the `ConsoleApp` project to start the bot.
