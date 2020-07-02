using Autofac;
using Autofac.Integration.WebApi;
using Movies.Interface;
using Movies.Service;
using Movies.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Movies.Web.App_Start
{
    public class ApiDIContainerConfig
    {
        public static class AutofacConfig
        {
            public static void Register()
            {
                var bldr = new ContainerBuilder();
                var config = GlobalConfiguration.Configuration;
                bldr.RegisterApiControllers(Assembly.GetExecutingAssembly());
                RegisterServices(bldr);
                bldr.RegisterWebApiFilterProvider(config);
                bldr.RegisterWebApiModelBinderProvider();
                var container = bldr.Build();
                config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            }

            private static void RegisterServices(ContainerBuilder bldr)
            {
                bldr.RegisterType<ApplicationDbContext>()
    .InstancePerRequest();

                bldr.RegisterType<MovieService>()
                   .As<IMovieService>()
                   .InstancePerRequest();

                bldr.RegisterType<TVService>()
                    .As<ITVService>()
                    .InstancePerRequest();

                bldr.RegisterType<FavoriteMovieService>()
                  .As<IFavoriteMovieService>()
                  .InstancePerRequest();
            }
        }
    }
}