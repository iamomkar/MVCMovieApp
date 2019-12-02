using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieAppSQL.Models;
using Autofac;
using MediatR;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using MovieAppSQL.Controllers;
using MediatR.Pipeline;
using MovieAppSQL.Models.DataAcessLayers;

namespace MovieAppSQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IDataAccessLayer, MovieDataAcessLayerEF>();


            //services.AddTransient<IRequestHandler<RequestModel, ResponseModelResult>, LoginHandler>();


            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddDbContext<MovieAppDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MovieDatabase")));
            services.AddControllersWithViews();
            /*
            //Meditor
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            
            var mediatrOpenTypes = new[]
              {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(LoginHandler).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }


            // It appears Autofac returns the last registered types first
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.RegisterType<Mediator>().As<IMediator>();
            builder.RegisterType<MovieDataAcessLayerEF>().As<IDataAccessLayer>();
            IContainer container = builder.Build();
            container.Resolve<IMediator>();*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
