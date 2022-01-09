using System.Text.Json.Serialization;

namespace FootballMatches.WebAPI.DTO
{
    public class LineupDTO
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public bool IsOnBench { get; set; }

        [JsonIgnore]
        public int Position { get; set; }
    }
}
