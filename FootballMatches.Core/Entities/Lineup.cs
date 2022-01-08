using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballMatches.Core.Entities
{
    public class Lineup
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Match")]
        public int MatchId { get; set; }
        public Match Match { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public bool IsOnBench { get; set; }
    }
}
