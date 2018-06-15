using CASportStore.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    // Access to configuration data via Configuration's key 
                    Configuration["Data:SportStoreProducts:ConnectionString"]) 
            );
            // Create new object each time the interface is needed
            services.AddTransient<IProductRepository, EfProductRepository>();
            // setup the shared objects used in MVC applications
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Display details of exception during development process. Should be disabled when deploying the app.
            app.UseDeveloperExceptionPage();
            // Add simple message  to HTTP responses e.g. 404 
            app.UseStatusCodePages();
            // Enable support for serving static content from wwwroot folder
            app.UseStaticFiles();
            // Enable ASP.NET Core MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "pagination",
                    template: "Product/Page{page}",
                    defaults: new { Controller = "Product", action="List"}
                    );
                routes.MapRoute(
                    name: "default", 
                    template: "{controller=Product}/{action=List}/{id?}"
                    );
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
