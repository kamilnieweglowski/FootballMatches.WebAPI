namespace FootballMatches.WebAPI.DTO
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CountryCode { get; set; }
        public int Position { get; set; }
        public int? TeamId { get; set; }
    }
}
