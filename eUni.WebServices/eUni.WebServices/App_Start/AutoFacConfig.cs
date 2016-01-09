using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers;
using eUni.DataAccess.eUniDbContext;
using eUni.DataAccess.Repository;
using Module = eUni.DataAccess.Domain.Module;

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

            builder.RegisterType<AspNetUserRepository>()
                .As<IAspNetUserRepository>();
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>();
            builder.RegisterType<CourseRepository>()
                .As<ICourseRepository>();
            builder.RegisterType<HomeworkRepository>()
                .As<IHomeworkRepository>();
            builder.RegisterType<ModuleRepository>()
                .As<IModuleRepository>();
            builder.RegisterType<WikiPageRepository>()
                .As<IWikiPageRepository>();
            builder.RegisterType<Repository<Module>>()
                .As<Repository<Module>>();
            builder.RegisterType<AspNetUserRepository>()
                .As<IAspNetUserRepository>();



            builder.RegisterType<UserProvider>()
                .As<IUserProvider>();
            builder.RegisterType<CourseProvider>()
                .As<ICourseProvider>();
            builder.RegisterType<ModuleProvider>()
                .As<IModuleProvider>();
            builder.RegisterType<WikiPageProvider>()
                .As<IWikiPageProvider>();
            builder.RegisterType<HomeworkProvider>()
                .As<IHomeworkProvider>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // add more dependencies here

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver =
                new AutofacWebApiDependencyResolver(container); //Set the WebApi DependencyResolver
        }
    }
}