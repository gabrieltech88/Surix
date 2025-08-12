using AutoMapper;
using Surix.Api.Data.Models;
using Surix.Api.Data.DTO;

namespace Surix.Api.Data.Profiles
{
    public class SureProfile : Profile
    {
        public SureProfile()
        {
            CreateMap<SureCreateRequest, Sure>();
        }
    }
}