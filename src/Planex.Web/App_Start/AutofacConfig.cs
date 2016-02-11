using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Planex.Data;
using Planex.Services.Images;
using Planex.Services.Skills;
using Planex.Services.Users;
using Planex.Web.Areas.HR.Controllers;

namespace Planex.Web
{
    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register(x => new PlanexDbContext())
                .As<DbContext>()
                .InstancePerLifetimeScope();
//            builder.Register(x => new HttpCacheService())
//                .As<ICacheService>()
//                .InstancePerRequest();
//            builder.Register(x => new IdentifierProvider())
//                .As<IIdentifierProvider>()
//                .InstancePerRequest();

            //var servicesAssembly = Assembly.GetAssembly(typeof(IJokesService));
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IUserService))).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IImageService))).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(ISkillService))).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();

//            builder.RegisterGeneric(typeof(PlanexData))
//                .As(typeof(IPlanexData))
//                .InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BaseController>().PropertiesAutowired();
        }
    }
}