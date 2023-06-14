using Demo.BLL.Interfaces;
using Demo.BLL.Reposatory;
using Demo.DAL.Context;
using Demo.DAL.Entity;
using Demo.PL.Mappers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL
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
            services.AddControllersWithViews();
            services.AddDbContext<MVCAppDbContext>(options =>
            {
                //  options.UseSqlServer("server=. ; database=MVCAppDbContext ; trusted_connection=true ;"); 

                options.UseSqlServer(Configuration.GetConnectionString("defaultconnection"));
            }); //Scopeed

            //services.AddTransient<IdepartmentReposatory, DepartmentReposatory>();
            //services.AddSingleton<IdepartmentReposatory, DepartmentReposatory>();
            services.AddScoped<IdepartmentReposatory, DepartmentReposatory>();
            services.AddScoped<IEmployeeRepository, EmployeeReposatory>();
            services.AddAutoMapper(M => M.AddProfile<EmployeeProfile>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentity<APPUser, IdentityRole>(Options =>
            {
                Options.Password.RequireDigit = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequireUppercase = true;
                Options.Password.RequiredLength = 5;
                Options.SignIn.RequireConfirmedAccount = false;
            })
                 .AddEntityFrameworkStores<MVCAppDbContext>()
             .AddTokenProvider<DataProtectorTokenProvider<APPUser>>(TokenOptions.DefaultProvider);
            //services.AddScoped<UserManager<APPUser>, UserManager<APPUser>>();
            //services.AddScoped<SignInManager<APPUser>, SignInManager<APPUser>>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(Options =>
                {
                    Options.LoginPath = "Account/Login";
                    Options.AccessDeniedPath = "Home/Error";
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
