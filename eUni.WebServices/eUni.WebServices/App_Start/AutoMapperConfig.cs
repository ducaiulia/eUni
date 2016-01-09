using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using eUni.BusinessLogic.AutoMapper;
using eUni.BusinessLogic.Providers.DataTransferObjects;
using eUni.WebServices.Models;

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
        }

        private static void HomeworkDTOToViewModel()
        {
            Mapper.CreateMap<HomeworkDTO, HomeworkViewModel>()
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