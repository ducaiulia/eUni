using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using eUni.DataAccess.eUniDbContext;
using eUni.DataAccess.Repository;

namespace eUni.WebServices.App_Start
{
    public class AutoFacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EUniDbContext>()
                .As<EUniDbContext>()
                .InstancePerRequest();

            builder.RegisterType<IUserRepository>()
                .As<UserRepository>();

            // add more dependencies here

            var container = builder.Build();
        }
    }
}