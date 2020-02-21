using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetEmployeeManamentApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetEmployeeManamentApplication
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
            services.AddDbContextPool<AppDBContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("EmployeeDBstring")));

            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 10;
                option.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<AppDBContext>();
            services.AddMvc(
                option =>
                {
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    option.Filters.Add(new AuthorizeFilter(policy));
                }
                );
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;


            //});


            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequiredLength = 10;
                option.Password.RequiredUniqueChars = 3;
            });

            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env
            , ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("MW1: Incoming Requsts ");
            //    await next();
            //    logger.LogInformation("MW1: Outgoing Responce ");
            //});

            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("MW2: Incoming Requsts ");
            //    await next();
            //    logger.LogInformation("MW2: Outgoing Responce ");
            //});
            /* This Code is when we need to change defult page and use Statice and app.Use Defulat FIle 
            FileServerOptions fileServerOptions = new FileServerOptions();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("Mypage.html");
            app.UseFileServer(fileServerOptions);
            */
            //app.UseFileServer();
            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseMvcWithDefaultRoute(); when we dont wnat to change defult Config
            app.UseMvc(route =>
            {
                route.MapRoute("default", "{Controller=Home}/{action=Index}/{id?}");
            });
            //app.Run(async (context) =>
            //{
            //    //throw new Exception("Some Error is processing");
            //    await context.Response.WriteAsync("Hello World!");
               
            //});

            //Commented By Ashish
            //app.UseStaticFiles();
            //app.UseCookiePolicy();

            //app.UseMvc();
        }
    }
}
