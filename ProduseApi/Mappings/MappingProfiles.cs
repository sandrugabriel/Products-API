using AutoMapper;
using ProduseApi.Models;
using ProduseApi.Dto;

namespace ProduseApi.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {


            CreateMap<CreateRequest, Produs>();
        }
    }
}
