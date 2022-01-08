using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballMatches.Core.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Position { get; set; }

        [ForeignKey("CountryCode")]
        public int CountryCodeId { get; set; }
        public CountryCode CountryCode { get; set; }

        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        public List<Lineup> Lineups { get; set; }
    }
}
