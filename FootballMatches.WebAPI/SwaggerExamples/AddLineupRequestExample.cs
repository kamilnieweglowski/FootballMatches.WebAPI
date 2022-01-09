using FootballMatches.WebAPI.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace FootballMatches.WebAPI.SwaggerExamples
{
    public class LineupDTOExample : IExamplesProvider<List<LineupDTO>>
    {
        public List<LineupDTO> GetExamples()
        {
            return Enumerable.Repeat(new LineupDTO() { MatchId = 0, TeamId = 0, PlayerId = 0, IsOnBench = false }, 11).ToList();
        }
    }
}