using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballMatches.Core.Entities
{
    public class CountryCode
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }


        public List<Player> Players { get; set; }
        public List<Team> Teams { get; set; }

        public List<Stadium> Stadiums { get; set; }
    }
}
