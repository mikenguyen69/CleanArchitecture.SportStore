﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using CASportStore.Core.Mappers;
using CASportStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using StructureMap;
using CASportStore.Core.Interfaces;
using CASportStore.Core.SharedKernel;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc;
using CASportStore.Infrastructure.Services;

namespace CASportStore.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"] 
        //        ??
        //        // To-do: testing doesn't seem getting the correct Configuration
        //        "Server=(localdb)\\MSSQLLocalDB;Database=SportsStore;Trusted_Connection=True;MultipleActiveResultSets=true")
        //    );
        //    services.AddMvc();          
        //    services.AddSingleton<IMapper>(_ => AutoMapperConfig.GetMapper());
            

        //    var container = new Container();
        //    container.Configure(config =>
        //    {
        //        config.Scan(_ =>
        //        {
        //            _.AssemblyContainingType(typeof(Startup)); // Web
        //            _.AssemblyContainingType(typeof(BaseEntity)); // Core
        //            _.Assembly("CASportStore.Infrastructure"); // Infrastructure
        //            _.WithDefaultConventions();
        //            _.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
        //        });

        //        config.For(typeof(IRepository<>)).Add(typeof(EfRepository<>));
                
        //        //Populate the container using the service collection
        //        config.Populate(services);
        //    });

        //    services.AddScoped<IRepository<Product>, EfRepository<Product>>();
        //    services.AddScoped<IRepository<ToDoItem>, EfRepository<ToDoItem>>();
        //    services.AddScoped<IProductService, ProductService>();
            
        //}

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            // TODO: Add DbContext and IOC
            string dbName = Guid.NewGuid().ToString();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(dbName));
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddSingleton<IMapper>(_ => AutoMapperConfig.GetMapper());
            services.AddScoped<IProductService, ProductService>();

            var container = new Container();

            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Startup)); // Web
                    _.AssemblyContainingType(typeof(BaseEntity)); // Core
                    _.Assembly("CASportStore.Infrastructure"); // Infrastructure
                    _.WithDefaultConventions();
                    _.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
                });

                // TODO: Add Registry Classes to eliminate reference to Infrastructure

                // TODO: Move to Infrastucture Registry
                config.For(typeof(IRepository<>)).Add(typeof(EfRepository<>));

                //Populate the container using the service collection
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }

        public void ConfigureTesting(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            this.Configure(app, env, loggerFactory);
            SeedData.PopulateTestData(app.ApplicationServices.GetService<ApplicationDbContext>());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            //Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
