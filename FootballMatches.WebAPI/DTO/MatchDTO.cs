namespace FootballMatches.WebAPI.DTO
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TeamDTO HomeTeam { get; set; }
        public TeamDTO AwayTeam { get; set; }
        public StadiumDTO Stadium { get; set; }

    }
}
