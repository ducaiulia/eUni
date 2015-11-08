using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers;
using eUni.DataAccess.eUniDbContext;
using eUni.DataAccess.Repository;

namespace eUni.WebServices
{
    public class AutoFacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ApplicationDbContext>()
                .As<ApplicationDbContext>()
                .InstancePerRequest();

            builder.RegisterType<UserRepository>()
                .As<IUserRepository>();
            builder.RegisterType<CourseRepository>()
                .As<ICourseRepository>();
            builder.RegisterType<AspNetUserRepository>()
                .As<IAspNetUserRepository>();



            builder.RegisterType<UserProvider>()
                .As<IUserProvider>();
            builder.RegisterType<CourseProvider>()
                .As<ICourseProvider>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // add more dependencies here

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver =
                new AutofacWebApiDependencyResolver(container); //Set the WebApi DependencyResolver
        }
    }
}