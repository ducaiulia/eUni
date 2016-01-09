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
                .ForMember(dest => dest.Teacher, opt=>opt.Ignore())
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.CourseCode))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate)).ReverseMap();
        }

        public static void WikiPageMapping()
        {
            Mapper.CreateMap<WikiPage, WikiPageDTO>();
            Mapper.CreateMap<WikiPageDTO, WikiPage>()
                .ForMember(dest => dest.Module, opt => opt.MapFrom(src => src.Module))
                .ForMember(dest => dest.WikiPageId, opt => opt.Ignore());
        }

        public static void ModuleMappings()
        {
            Mapper.CreateMap<Module, ModuleDTO>()
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => Mapper.Map<CourseDTO>(src.Course)))
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents)).ReverseMap();

            Mapper.CreateMap<ModuleDTO, Module>()
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId));
        }

        public static void HomeworkMappings()
        {
            Mapper.CreateMap<Homework, HomeworkDTO>()
                .ForMember(dest => dest.HomeworkId, opt => opt.MapFrom(src => src.HomeworkId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score)).ReverseMap();
        }



        public static void ContentMappings()
        {
            Mapper.CreateMap<Content, ContentDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }

    }
}
