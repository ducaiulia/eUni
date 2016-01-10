using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using eUni.BusinessLogic.AutoMapper;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Models;
using eUni.WebServices.Models.OutputModels;

namespace eUni.WebServices
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            BusinessLogicMapper.UserMappings();
            BusinessLogicMapper.CourseMappings();
            BusinessLogicMapper.ModuleMappings();
            BusinessLogicMapper.ContentMappings();
            BusinessLogicMapper.HomeworkMappings();
            BusinessLogicMapper.WikiPageMapping();
            BusinessLogicMapper.TestMappings();
            BusinessLogicMapper.FileMappings();
            BusinessLogicMapper.StudentHWMappings();
            BusinessLogicMapper.QuestionMappings();
            BusinessLogicMapper.AnswerMappings();
            BusinessLogicMapper.StudentTestMapping();
            BusinessLogicMapper.MessageMapping();
            StudentHomeworkDTOToViewModel();
            UserDTOToViewModel();
            CourseDTOToViewModel();
            HomeworkDTOToViewModel();
            ModuleDTOToViewModel();
            WikiPageDTOToViewModel();
            TestDTOToViewModel();
            QuestionDTOToViewModel();
            FileDTOToViewModel();
            EntityDTOToNamedEntityModels();
            AnswerDTOToViewModel();
            MessageDTOToViewModel();


        }

        private static void MessageDTOToViewModel()
        {
            Mapper.CreateMap<MessageViewModel, MessageDTO>();

        }

        private static void StudentHomeworkDTOToViewModel()
        {
            Mapper.CreateMap<StudentHomeworkDTO, StudentHomeworkViewModel>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.HomeworkId, opt => opt.MapFrom(src => src.HomeworkId))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade)).ReverseMap();
        }

        private static void StudentTestDTOToViewModel()
        {
            Mapper.CreateMap<StudentTestDTO, StudentTestViewModel>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade)).ReverseMap();
        }

        private static void EntityDTOToNamedEntityModels()
        {
            Mapper.CreateMap<ModuleDTO, NamedEntityOutModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ModuleId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


        }

        private static void FileDTOToViewModel()
        {
            Mapper.CreateMap<FileDTO, FileViewModel>();
            Mapper.CreateMap<FileDTO, FileOutModel>();

        }

        private static void QuestionDTOToViewModel()
        {
            Mapper.CreateMap<QuestionDTO, QuestionViewModel>()
                 .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
                 .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                 .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
                 .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score)).ReverseMap();
        }

        private static void TestDTOToViewModel()
        {
            Mapper.CreateMap<TestDTO, TestViewModel>()
                 .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();
        }

        private static void ModuleDTOToViewModel()
        {
            Mapper.CreateMap<ModuleDTO, ModuleViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.Course.CourseCode)).ReverseMap();
        }


        private static void WikiPageDTOToViewModel()
        {
            Mapper.CreateMap<WikiPageDTO, WikiPageViewModel>()
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.Module.ModuleId));

            Mapper.CreateMap<WikiPageViewModel, WikiPageDTO>();

            Mapper.CreateMap<WikiPageDTO, WikiPageOutModel>();


        }

        private static void HomeworkDTOToViewModel()
        {
            Mapper.CreateMap<HomeworkDTO, HomeworkViewModel>()
                .ForMember(dest => dest.HomeworkId, opt => opt.MapFrom(src => src.HomeworkId))
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.Module.ModuleId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score)).ReverseMap();
        }


        public static void UserDTOToViewModel()
        {
            Mapper.CreateMap<DomainUserDTO, UserViewModel>()
                .ForMember(dest => dest.DomainUserId, opt => opt.MapFrom(src => src.DomainUserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.MatriculationNumber, opt => opt.MapFrom(src => src.MatriculationNumber)).ReverseMap();
        }
        public static void CourseDTOToViewModel()
        {
            Mapper.CreateMap<CourseDTO, CourseViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.CourseCode)).ReverseMap();

        }

        public static void AnswerDTOToViewModel()
        {
            Mapper.CreateMap<AnswerDTO, AnswerViewModel>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.AnswerId, opt => opt.MapFrom(src => src.AnswerId))
                .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect)).ReverseMap();
        }
    }
}