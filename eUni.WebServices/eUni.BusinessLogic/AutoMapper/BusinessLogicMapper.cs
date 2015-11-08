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
                .ForMember(dest => dest.MatriculationNumber, opt => opt.MapFrom(src => src.MatriculationNumber)).ReverseMap();
        }
        public static void CourseMappings()
        {
            Mapper.CreateMap<Course, CourseDTO>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Teacher, opt=>opt.Ignore())//MapFrom(src=>src.Teacher))
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.CourseCode))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate)).ReverseMap();
        }

        public static void ModuleMappings()
        {
            Mapper.CreateMap<Module,ModuleDTO>()
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => Mapper.Map<CourseDTO>(src.Course)))
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents));
        }

        public static void ContentMappings()
        {
            Mapper.CreateMap<Content,ContentDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }

    }
}
