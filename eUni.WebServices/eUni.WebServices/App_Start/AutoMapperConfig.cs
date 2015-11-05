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
            UserDTOToViewModel();
        }

        public static void UserDTOToViewModel()
        {
            Mapper.CreateMap<DomainUserDTO, UserViewModel> ()
                .ForMember(dest => dest.DomainUserId, opt => opt.MapFrom(src => src.DomainUserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.MatriculationNumber, opt => opt.MapFrom(src => src.MatriculationNumber));
        }
    }
}