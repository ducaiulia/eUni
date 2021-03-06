﻿using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using eUni.BusinessLogic.IProviders;
using eUni.BusinessLogic.Providers;
using eUni.DataAccess.Domain;
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

            builder.RegisterType<UserRepository>()
                .As<IUserRepository>();
            builder.RegisterType<CourseRepository>()
                .As<ICourseRepository>();
            builder.RegisterType<HomeworkRepository>()
                .As<IHomeworkRepository>();
            builder.RegisterType<ModuleRepository>()
                .As<IModuleRepository>();
            builder.RegisterType<LogsRepository>()
                .As<ILogsRepository>();
            builder.RegisterType<QuestionRepository>()
                .As<IQuestionRepository>();
            builder.RegisterType<TestRepository>()
                .As<ITestRepository>();
            builder.RegisterType<AnswerRepository>()
                .As<IAnswerRepository>();
            builder.RegisterType<WikiPageRepository>()
                .As<IWikiPageRepository>();
            builder.RegisterType<AspNetUserRepository>()
                .As<IAspNetUserRepository>();
            builder.RegisterType<FileRepository>()
                .As<IFileRepository>();
            builder.RegisterType<StudentHomeworkRepository>()
                .As<IStudentHomeworkRepository>();
            builder.RegisterType<StudentTestRepository>()
                .As<IStudentTestRepository>();
            builder.RegisterType<MessageRepository>()
                .As<IMessageRepository>();

            builder.RegisterType<UserProvider>()
                .As<IUserProvider>();
            builder.RegisterType<CourseProvider>()
                .As<ICourseProvider>();
            builder.RegisterType<LogProvider>()
                .As<ILogProvider>();
            builder.RegisterType<ModuleProvider>()
                .As<IModuleProvider>();
            builder.RegisterType<WikiPageProvider>()
                .As<IWikiPageProvider>();
            builder.RegisterType<HomeworkProvider>()
                .As<IHomeworkProvider>();
            builder.RegisterType<QuestionProvider>()
                .As<IQuestionProvider>();
            builder.RegisterType<TestProvider>()
                .As<ITestProvider>();
            builder.RegisterType<FileProvider>()
                .As<IFileProvider>();
            builder.RegisterType<AnswerProvider>()
                .As<IAnswerProvider>();
            builder.RegisterType<StudentHomeworkProvider>()
                .As<IStudentHomeworkProvider>();
            builder.RegisterType<MessageProvider>()
                .As<IMessageProvider>();
            builder.RegisterType<StudentTestProvider>().
                As<IStudentTestProvider>();


            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // add more dependencies here

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver =
                new AutofacWebApiDependencyResolver(container); //Set the WebApi DependencyResolver
        }
    }
}