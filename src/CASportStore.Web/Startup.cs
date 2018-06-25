using CASportStore.Core.Entities;
using CASportStore.Core.Interfaces;
using CASportStore.Core.Services;
using CASportStore.Core.SharedKernel;
using CASportStore.Infrastructure.Data;
using CASportStore.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace CASportStore.Web
{
    public class Startup
    {
        #region To-do

        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    services.Configure<CookiePolicyOptions>(options =>
        //    {
        //        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        //        options.CheckConsentNeeded = context => true;
        //        options.MinimumSameSitePolicy = SameSiteMode.None;
        //    });
        //    // TODO: Add DbContext and IOC
        //    string dbName = Guid.NewGuid().ToString();
        //    services.AddDbContext<AppDbContext>(options =>
        //        options.UseInMemoryDatabase(dbName));
        //        //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        //    services.AddMvc()
        //        .AddControllersAsServices()
        //        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
        //    });

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

        //        // TODO: Add Registry Classes to eliminate reference to Infrastructure

        //        // TODO: Move to Infrastucture Registry
        //        config.For(typeof(IRepository<>)).Add(typeof(EfRepository<>));

        //        //Populate the container using the service collection
        //        config.Populate(services);
        //    });

        //    return container.GetInstance<IServiceProvider>();
        //}

        //public void ConfigureTesting(IApplicationBuilder app,
        //    IHostingEnvironment env,
        //    ILoggerFactory loggerFactory)
        //{
        //    this.Configure(app, env, loggerFactory);
        //    SeedData.PopulateTestData(app.ApplicationServices.GetService<AppDbContext>());
        //}


        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        //{
        //    loggerFactory.AddConsole(Configuration.GetSection("Logging"));
        //    loggerFactory.AddDebug();

        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //        app.UseHsts();
        //    }

        //    app.UseHttpsRedirection();
        //    app.UseStaticFiles();
        //    app.UseCookiePolicy();

        //    //Enable middleware to serve generated Swagger as a JSON endpoint.
        //    app.UseSwagger();

        //    //Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        //    });

        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //            name: "default",
        //            template: "{controller=Home}/{action=Index}/{id?}");
        //    });
        //}
        #endregion

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        // Setup shared objects that can be used throughout the application through DI
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    // Access to configuration data via Configuration's key 
                    Configuration["Data:SportStoreProducts:ConnectionString"]) 
            );

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:SportStoreIdentity:ConnectionString"]
                    )
            );
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            // Specify the same object should be used to satisfy related requests for Cart instances
            services.AddScoped<CartService>(sp => SessionCart.GetCart(sp));
            // Specify the same object should always be used
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Create new object each time the interface is needed
            // setup the shared objects used in MVC applications
            services.AddMvc();
            // setup in memory data store
            services.AddMemoryCache();
            // register services to access session data
            services.AddSession();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Display details of exception during development process. Should be disabled when deploying the app.
                app.UseDeveloperExceptionPage();
                // Add simple message  to HTTP responses e.g. 404 
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            
            // Enable support for serving static content from wwwroot folder
            app.UseStaticFiles();
            // Use session
            app.UseSession();
            // Setup component that intercept request and response to implement the security policy
            app.UseAuthentication();
            // Enable ASP.NET Core MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: null,
                     template: "{category}/Page{productPage:int}",
                     defaults: new { controller = "Product", action = "List" }
                );

                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                );

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                );

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Product", action = "List", page = 1 });

                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");

            });
            //SeedData.EnsurePopulated(app);
            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}
