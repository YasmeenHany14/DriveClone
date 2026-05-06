using AutoMapper;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.Application.DTOs.UserDtos;
using DriveClone.Domain.Models;

namespace DriveClone.Application.Mappers.MappingProfiles;

public class UserMappingProfiles : Profile
{
    public UserMappingProfiles()
    {
        CreateMap<CreateUserAppDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));

        CreateMap<User, GetUserByIdAppDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));
    }
}
