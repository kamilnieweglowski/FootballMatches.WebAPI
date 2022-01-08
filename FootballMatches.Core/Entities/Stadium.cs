using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballMatches.Core.Entities
{
    public class Stadium
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public List<Match> Matches { get; set; }
    }
}
