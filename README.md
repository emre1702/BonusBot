# BonusBot
Discord bot written in C# using the framework [Discord.Net](https://github.com/discord-net/Discord.Net).  

## What exactly is BonusBot?  
BonusBot has been written to do some stuff in Discord like for example:  
* Play music from YouTube  
* Delete multiple messages at once  
* Ban people for a specific time and inform them  
* Announce game planings for one of our Discord servers  
* Connect Discord with my private GTA 5 (RAGE:MP) server TDS-V  
* Output when someone leaves the server  
* ... and much more  

My [previous repository](https://github.com/emre1702/BonusBot_Csharp) could do all of them and maybe more (depends on at which date you are reading this).  
But the code was terrible so I decided to rewrite the whole code.  
Well, atleast it was much better than my [first BonusBot](https://github.com/emre1702/BonusBot_Java) :)  

## How can I get the bot?
You can't invite the bot I'm already running for my private servers.  
I honestly don't have a server with enough resources to be able to handle many guilds.  

But you can:  
Clone this repository  
or  
Use my [Docker image](https://hub.docker.com/repository/docker/emre1702/bonusbot) directly.  

### Environment variables:  
1. BONUSBOT_TOKEN  
The [token of the bot](https://github.com/reactiflux/discord-irc/wiki/Creating-a-discord-bot-&-getting-a-token).

## How is the code structured?  
There are the main, systems and module projects.

The main projects contain the logic to make BonusBot function for any functionality:  
* Core:  
Startup, Initialization, Dependency Injection, module references
* Common:  
Common logic for everything.  
* Database:  
Entity Framework Core.  
* Services:  
Services needed for BonusBot to work.  

System projects are wrapper for Discord classes to mostly improve speed of the bot with caching and make the code better.  

Modules contain all the functionalities and commands.  
They can be added and removed easily, need to be referenced from Core and need to end with 'Module'.  
The module 'ModulesControllingModule' allows the guild administrators to enable and disable the modules for their guild.  


## Other used dependencies:  
* [Lavalink](https://github.com/Frederikam/Lavalink):  
Used in AudioModule for sending audio from e.g. YouTube or Soundcloud  
* [gRPC](https://github.com/grpc/grpc-dotnet) with [Google.Protobuf](https://developers.google.com/protocol-buffers/docs/csharptutorial):  
For my private modules to allow sending to and receiving from my other project.  
* [Macross.Json.Extensions](https://blog.macrosssoftware.com/):   
To be able to convert string to enum or vice versa with custom names in System.Text.Json.  
* [Colorful.Console](https://github.com/tomakita/Colorful.Console):   
Enhanced styling functionality for the console.  
* [MoreLINQ](https://github.com/morelinq/MoreLINQ):  
More functionalities for LINQ.  
* [Docker](https://hub.docker.com/):  
This code is written to work in a Docker container. If you don't want to use it, you can modify the code - it should be pretty easy.  
E.g. the ConnectionString in BonusDbContextFactory.cs uses an absolute path for a Docker volume, modify it.  
* ... and more from .NET Team  
