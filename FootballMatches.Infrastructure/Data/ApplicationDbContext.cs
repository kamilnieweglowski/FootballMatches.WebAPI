using Microsoft.EntityFrameworkCore;
using FootballMatches.Core.Entities;
using Bogus;
using FootballMatches.Core.Enums;
using System.ComponentModel;

namespace FootballMatches.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Lineup> Lineups { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<CountryCode> CountryCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                    .HasOne(p => p.Team)
                    .WithMany(t => t.Players);

            modelBuilder.Entity<Player>()
                    .HasOne(p => p.CountryCode);

            modelBuilder.Entity<Team>()
                    .HasMany(p => p.Players)
                    .WithOne(t => t.Team)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Team>()
                    .HasOne(p => p.CountryCode);

            modelBuilder.Entity<Team>()
                    .HasOne(p => p.Stadium);

            modelBuilder.Entity<Match>()
                    .HasOne(m => m.HomeTeam)
                    .WithMany(t => t.HomeMatches)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Match>()
                    .HasOne(m => m.AwayTeam)
                    .WithMany(t => t.AwayMatches)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Match>()
                    .HasOne(m => m.Stadium)
                    .WithMany(t => t.Matches)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Lineup>()
                    .HasOne(m => m.Match)
                    .WithMany(t => t.Lineups)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Lineup>()
                    .HasOne(m => m.Player)
                    .WithMany(t => t.Lineups)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Lineup>()
                    .HasOne(m => m.Team)
                    .WithMany(t => t.Lineups)
                    .OnDelete(DeleteBehavior.ClientSetNull);


            //seed data
            //CountryCodes
            Array countryCodeValues = Enum.GetValues(typeof(CountryCodes));
            List<CountryCode> countryCodes = new List<CountryCode>();
            var i = 1;
            foreach (var countryCode in countryCodeValues)
            {
                var type = countryCode.GetType();
                var memInfo = type.GetMember(countryCode.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var desc = (attributes.Length > 0) ? (DescriptionAttribute)attributes[0] : null;


                countryCodes.Add(new CountryCode() { Id = i, Code = countryCode.ToString(), Country = desc.Description });
                i++;
            }
            modelBuilder.Entity<CountryCode>().HasData(countryCodes);

            //Stadiums
            Stadium[] stadiums = new Stadium[6];
            i = 1;

            var testStadium = new Faker<Stadium>()
                .RuleFor(u => u.Id, (f, u) => i)
                .RuleFor(u => u.Name, (f, u) => f.Company.CompanyName())
                .RuleFor(u => u.City, (f, u) => f.Address.City());

            for (i = 1; i <= 6; i++)
            {
                var stadium = testStadium.Generate();
                stadiums[i - 1] = stadium;
            }
            modelBuilder.Entity<Stadium>().HasData(stadiums);

            //Teams
            var catchySuffixes = new string[3] { "United", "City", "F.C." };
            Team[] teams = new Team[6];
            i = 1;

            Random random = new Random();
            int randomCountryCode = random.Next(1, 249);

            var testTeam = new Faker<Team>()
                .RuleFor(u => u.Id, (f, u) => i)
                .RuleFor(u => u.Name, (f, u) => f.Company.CompanyName() + " " + f.PickRandom(catchySuffixes))
                .RuleFor(u => u.CountryCodeId, f => randomCountryCode)
                .RuleFor(u => u.StadiumId, f => i);

            for (i = 1; i <= 6; i++)
            {
                var team = testTeam.Generate();
                teams[i - 1] = team;
                randomCountryCode = random.Next(1, 249);
            }
            modelBuilder.Entity<Team>().HasData(teams);

            //Players
            var playerId = 1;
            Player[] players = new Player[108];
            var testPlayer = new Faker<Player>()
                .RuleFor(u => u.Id, (f, u) => playerId)
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(0))
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(0))
                .RuleFor(u => u.DateOfBirth, f => f.Date.Past(40, DateTime.Today.AddYears(-25)))
                .RuleFor(u => u.CountryCodeId, f => randomCountryCode)
                .RuleFor(u => u.TeamId, f => i);

            foreach (var t in teams)
            {
                for(var j = 1; j<= 18; j++)
                {
                    var player = testPlayer.Generate();
                    player.TeamId = t.Id;

                    switch(j)
                    {
                        case 1:
                            player.Position = 1;
                            break;
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            player.Position = 2;
                            break;
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            player.Position = 3;
                            break;
                        case 10:
                        case 11:
                            player.Position = 4;
                            break;
                        default:
                            player.Position = random.Next(1, 4);
                            break;
                    }
                    players[playerId-1] = player;
                    randomCountryCode = random.Next(1, 249);
                    playerId++;
                } 
            }
            modelBuilder.Entity<Player>().HasData(players);

            //Matches
            Match[] matches = new Match[3];
            i = 1;

            var testMatch = new Faker<Match>()
                .RuleFor(u => u.Id, (f, u) => i++)
                .RuleFor(u => u.Date, f => f.Date.Past());

            var match = testMatch.Generate();
            match.HomeTeamId = 1;
            match.AwayTeamId = 2;
            match.StadiumId = 1;
            matches[0] = match;

            match = testMatch.Generate();
            match.HomeTeamId = 3;
            match.AwayTeamId = 4;
            match.StadiumId = 2;
            matches[1] = match;

            match = testMatch.Generate();
            match.HomeTeamId = 5;
            match.AwayTeamId = 6;
            match.StadiumId = 3;
            matches[2] = match;

            modelBuilder.Entity<Match>().HasData(matches);

            //Lineups
            Lineup[] lineups = new Lineup[108];
            var lineupId = 1;
            var teamId = 1;

            var testLineup = new Faker<Lineup>()
                .RuleFor(u => u.Id, (f, u) => lineupId);

            foreach(var m in matches)
            {
                for(var j = 1; j <= 2; j++)
                {
                    for(var k = 1; k <= 18; k++)
                    {
                        var lineup = testLineup.Generate();
                        lineup.MatchId = m.Id;
                        lineup.TeamId = teamId;
                        lineup.PlayerId = lineupId;
                        lineup.IsOnBench = (k > 11);
                        lineups[lineupId - 1] = lineup;

                        lineupId++;
                    }
                    teamId++;
                }
            }
            modelBuilder.Entity<Lineup>().HasData(lineups);
        }
    }
}
