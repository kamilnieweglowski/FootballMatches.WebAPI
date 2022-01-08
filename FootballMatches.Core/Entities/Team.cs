﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballMatches.Core.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("CountryCode")]
        public int CountryCodeId { get; set; }
        public CountryCode CountryCode { get; set; }

        public List<Player> Players { get; set; }

        public List<Lineup> Lineups { get; set; }

        [InverseProperty(nameof(Match.HomeTeam))]
        public List<Match> HomeMatches { get; set; }
        [InverseProperty(nameof(Match.AwayTeam))]
        public List<Match> AwayMatches { get; set; }
    }
}