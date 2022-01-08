using Microsoft.EntityFrameworkCore;
using FootballMatches.Core.Entities;
using Bogus;
using FootballMatches.Core.Enums;

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
            //Teams
            var catchySuffixes = new string[3] { "United", "City", "F.C." };
            Team[] teams = new Team[6];
            var i = 1;

            Random random = new Random();
            int randomCountryCode = random.Next(1, 249);

            Array positionValues = Enum.GetValues(typeof(Positions));
            int randomPosition = (int)positionValues.GetValue(random.Next(positionValues.Length));

            var testTeam = new Faker<Team>()
                .RuleFor(u => u.Id, (f, u) => i)
                .RuleFor(u => u.Name, (f, u) => f.Company.CompanyName() + " " + f.PickRandom(catchySuffixes))
                .RuleFor(u => u.CountryCodeId, f => randomCountryCode);

            for (i = 1; i <= 6; i++)
            {
                var team = testTeam.Generate();
                teams[i - 1] = team;
                randomCountryCode = random.Next(1, 249);
            }
            modelBuilder.Entity<Team>().HasData(teams);

            //Players
            var teamId = 1;
            Player[] players = new Player[108];
            var testPlayer = new Faker<Player>()
                .RuleFor(u => u.Id, (f, u) => i)
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(0))
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(0))
                .RuleFor(u => u.DateOfBirth, f => f.Date.Past(40, DateTime.Today.AddYears(-25)))
                .RuleFor(u => u.CountryCodeId, f => randomCountryCode)
                .RuleFor(u => u.Position, f => randomPosition)
                .RuleFor(u => u.TeamId, f => teamId);

            for (i = 1; i <= 108; i++)
            {
                var player = testPlayer.Generate();
                players[i - 1] = player;

                if (i % 18 == 0)
                    teamId++;

                randomCountryCode = random.Next(1, 249);
            }
            modelBuilder.Entity<Player>().HasData(players);
        }
    }
}
