using AutoMapper;
using FootballMatches.Core.Entities;
using FootballMatches.WebAPI.DTO;

namespace FootballMatches.WebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Player, PlayerDTO>();
            CreateMap<Team, TeamDTO>();
            CreateMap<Match, MatchDTO>();
            CreateMap<CountryCode, CountryCodeDTO>();
            CreateMap<Lineup, LineupDTO>();
            CreateMap<Stadium, StadiumDTO>();

            CreateMap<PlayerDTO, Player>();
            CreateMap<TeamDTO, Team>();
            CreateMap<MatchDTO, Match>();
            CreateMap<CountryCodeDTO, CountryCode>();
            CreateMap<LineupDTO, Lineup>();
            CreateMap<StadiumDTO, Stadium>();
        }
    }
}