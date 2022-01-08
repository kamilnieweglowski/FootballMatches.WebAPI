namespace FootballMatches.WebAPI.DTO
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int? StadiumId { get; set; }

    }
}
