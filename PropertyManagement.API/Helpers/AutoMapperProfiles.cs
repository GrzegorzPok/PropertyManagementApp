using System.Linq;
using AutoMapper;
using PropertyManagement.API.Dtos;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Property, PropertyForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.isMain).Url));
            CreateMap<Property, PropertyForDetailedDto>() 
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.isMain).Url));
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<PhotosForDetailedDto, Photo>();
            CreateMap<PropertyForDetailedDto, Property>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
        } 
    }
}