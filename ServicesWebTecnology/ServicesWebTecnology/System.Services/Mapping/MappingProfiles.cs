using AutoMapper;
using System.Infrastructure.DTO;
using SystemQuickzal.Data.Entity;

namespace System.Infrastructure.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Compositioncategory, CompositioncategoryDto>().ReverseMap();
        }

    }

}
