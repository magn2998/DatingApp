using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotosForCreationDto, Photo>();
            CreateMap<UserForeRegisterDto, User>();
            CreateMap<UserLikersDto, Like>().ReverseMap();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Posts, PostToReturnDto>().ReverseMap();
            CreateMap<Posts, PostlikeToReturnListDto>().ReverseMap();
            CreateMap<ProfileLikedPostToReturnDto, Postlike>().ReverseMap();
            CreateMap<Postlike, PostlikeToReturnListDto>().ReverseMap();
            CreateMap<Posts, PostForCreationDto>().ReverseMap();
            CreateMap<Posts, PostForCreationDto>().ReverseMap();
            CreateMap<ProfileToReturnDto, Profiles>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
            CreateMap<ProfileForRegisterDto, Profiles>();
            CreateMap<PostCreatedToReturnDto, PostlikeToReturnListDto>().ReverseMap();
            CreateMap<ProfileDetailedToReturn, PostCreatedToReturnDto>().ReverseMap();
            CreateMap<ProfileLikedPostToReturnDto, ProfileDetailedToReturn>().ReverseMap();
            CreateMap<ProfileDetailedToReturn, Profiles>().ReverseMap();
            CreateMap<Posts, PostCreatedToReturnDto>().ReverseMap();
                
        }
    }
}