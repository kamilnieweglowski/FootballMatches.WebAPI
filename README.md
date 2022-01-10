# FootballMatches.WebAPI
Here's small and simple .NET Core Web API for dealing with football matches!

You can use it for getting, adding, editing and deleting players, teams, matches, lineups and stadiums 
(I've decided to extend the first model a little to make it a bit closer to reality).

DB was created using Code First approach. In the project I've used Sqlite db and Bogus ver.34.0.1 for generating some dummy initial data.

Diagram for the db:

![alt text](https://github.com/kamilnieweglowski/FootballMatches.WebAPI/blob/master/dbDiagram.png?raw=true)

Some important conventions
- Player could belong to one team at a time, but he can also exist without it
- Team could have any number of Players and optionally can have a Stadium. More than one team can have the same Stadium.
- Match could be created without the knowledge where it would be played - but HomeTeam, AwayTeam and Date are required.
- Lineups could have up to 18 players - min. 11 Players in main lineup plus max. 7 players on bench. Also, each lineup has to have at least one goalkeeper.
- There are four main positions for Players: Goalkeeper, Defender, Midfielder and Forward.

Unfortunately, I didn't have enough time to accomplish everything I wanted to. For example, operations on dbContext could've been moved from controllers to 
separate services (e.g. MatchesService, TeamsService etc.), plus there should've been added tests both for Web API and entity manager.

# How to quickly test it
Please clone the repository, then navigate in cmd to FootballMatches.WebAPI folder, where FootballMatches.WebAPI.csproj is present, and run:

dotnet restore

dotnet run

Then, please navigate to https://localhost:7023/swagger/index.html or http://localhost:5023/swagger/index.html.
