using AutoMapper;
using DbFirst.Models.DTO;

namespace DbFirst.Helpers
{
    public class Helper : Profile
    {
        public Helper() {
            CreateMap<Team, TeamDTO>();
            CreateMap<TeamDTO, Team>();
        }
    }
}
