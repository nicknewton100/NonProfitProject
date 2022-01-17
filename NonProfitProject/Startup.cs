using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using NonProfitProject.Code;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NonProfitProject.Code.Security;
using NonProfitProject.Models.Settings;

namespace NonProfitProject
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

            services.AddTransient<BackgroundTasks>();

            services.AddMemoryCache();

            services.AddSession();

            services.AddControllersWithViews();           
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //creates new view page directories that the program searches through when looking for a view
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Add("/Views/Shared/Employee/{1}/{0}" + RazorViewEngine.ViewExtension);
                options.AreaViewLocationFormats.Add("/Views/Shared/Member/{1}/{0}" + RazorViewEngine.ViewExtension);
                options.AreaViewLocationFormats.Add("/Views/Shared/Users/{1}/{0}" + RazorViewEngine.ViewExtension);
            });


            //adds the database connection string
            services.AddDbContext<NonProfitContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("NonProfitContext"))
            );
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailManager, EmailManager>();
            services.Configure<AdminAccountInformation>(Configuration.GetSection("AdminAccountInformation"));
            //sets the password requirements
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<NonProfitContext>()
                .AddDefaultTokenProviders();
            //adds the appending trailing slash option and lowercase urls
            services.AddRouting(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });
            
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {   //Maps Admin Area to the pattern
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "users",
                    areaName: "Users",
                    pattern: "User/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "employee",
                    areaName: "Employee",
                    pattern: "Employee/{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            NonProfitContext.CreateAdminUser(app.ApplicationServices,Configuration).Wait();
        }
    }
}
