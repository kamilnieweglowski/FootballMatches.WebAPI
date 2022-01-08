using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballMatches.Core.Entities
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("HomeTeam")]
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        [ForeignKey("AwayTeam")]
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        [ForeignKey("Stadium")]
        public int StadiumId { get; set; }
        public Stadium Stadium { get; set; }

        public List<Lineup> Lineups { get; set; }
    }
}
