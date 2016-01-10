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

            Mapper.CreateMap<ModuleDTO, Module>()
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId)).ReverseMap();
        }

        public static void HomeworkMappings()
        {
            Mapper.CreateMap<Homework, HomeworkDTO>()
                .ForMember(dest => dest.HomeworkId, opt => opt.MapFrom(src => src.HomeworkId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score)).ReverseMap();
        }

        public static void FileMappings()
        {
            Mapper.CreateMap<File, FileDTO>()
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.Module.ModuleId))
                .ReverseMap();
        }

        public static void ContentMappings()
        {
            Mapper.CreateMap<Content, ContentDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }

        public static void AnswerMappings()
        {
            Mapper.CreateMap<Answer, AnswerDTO>()
                .ForMember(dest => dest.AnswerId, opt => opt.MapFrom(src => src.AnswerId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect)).ReverseMap();
        }

        public static void QuestionMappings()
        {
            Mapper.CreateMap<Question, QuestionDTO>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.Module.ModuleId))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score)).ReverseMap();
        }

        public static void TestMappings()
        {
            Mapper.CreateMap<Test, TestDTO>()
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.Module.ModuleId))
                .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();
        }

        public static void StudentHWMappings()
        {
            Mapper.CreateMap<StudentHomework, StudentHomeworkDTO>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.DomainUserId))
                .ForMember(dest => dest.HomeworkId, opt => opt.MapFrom(src => src.HomeworkId))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ReverseMap();
        }
    }
}
