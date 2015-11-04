using AutoMapper;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.DataAccess.Domain;

namespace eUni.BusinessLogic.AutoMapper
{
    public class BusinessLogicMapper
    {
        public static void UserMappings()
        {
            Mapper.CreateMap<DomainUser, DomainUserDTO>()
                .ForMember(dest => dest.DomainUserId, opt => opt.MapFrom(src => src.DomainUserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.MatriculationNumber, opt => opt.MapFrom(src => src.MatriculationNumber));
        }
    }
}
