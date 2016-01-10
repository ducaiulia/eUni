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
            UserDTOToViewModel();
            CourseDTOToViewModel();
            HomeworkDTOToViewModel();
            ModuleDTOToViewModel();
            WikiPageDTOToViewModel();
            TestDTOToViewModel();
            QuestionDTOToViewModel();
            FileDTOToViewModel();
            EntityDTOToNamedEntityModels();

        }

        private static void EntityDTOToNamedEntityModels()
        {
            Mapper.CreateMap<ModuleDTO, NamedEntityOutModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ModuleId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


        }

        private static void FileDTOToViewModel()
        {
            Mapper.CreateMap<FileDTO, FileViewModel>()
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => src.Description));
            Mapper.CreateMap<FileDTO, FileOutModel>();

        }

        private static void QuestionDTOToViewModel()
        {
            Mapper.CreateMap<QuestionDTO, QuestionViewModel>()
                 .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.Module.ModuleId))
                 .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
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
    }
}