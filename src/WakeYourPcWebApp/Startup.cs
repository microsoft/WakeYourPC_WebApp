using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WakeYourPcWebApp.Models;
using Newtonsoft.Json.Serialization;
using WakeYourPcWebApp.Controllers.Api.v1;
using WakeYourPcWebApp.ViewModels;
using WakeYourPcWebApp.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;

namespace WakeYourPcWebApp
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private readonly IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            services.AddDbContext<WakeupContext>();

            services.AddScoped<IWakeupRepository, WakeupRepository>();

            services.AddTransient<WakeupContextSeedData>();

            services.AddLogging();

            services.AddMvc().AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IHostingEnvironment env, 
            WakeupContextSeedData seeder,
            WakeupContext context)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<MachineViewModel, Machine>().ReverseMap();
                config.CreateMap<UserViewModel, User>().ReverseMap();
            });


            //if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                );

                //config.MapRoute(
                //    name: "ApiDefault",
                //    template: "api/v1/{controller}/{username}/{action}",
                //    defaults: new { action = "GetUser" }
                //);

            });

            context.Database.Migrate();

            seeder.EnsureSeedData().Wait();

        }
    }
}
