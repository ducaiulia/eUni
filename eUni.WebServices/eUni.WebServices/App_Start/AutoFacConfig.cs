using Autofac;
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

            // add more dependencies here

            var container = builder.Build();
        }
    }
}