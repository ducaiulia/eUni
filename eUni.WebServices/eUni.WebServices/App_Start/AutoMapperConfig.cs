using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eUni.BusinessLogic.AutoMapper;

namespace eUni.WebServices
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            BusinessLogicMapper.UserMappings();
        }
    }
}