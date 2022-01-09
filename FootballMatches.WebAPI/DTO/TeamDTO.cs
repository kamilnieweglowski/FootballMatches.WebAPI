namespace FootballMatches.WebAPI.DTO
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int CountryCodeId { get; set; }
        public int? StadiumId { get; set; }
    }
}
