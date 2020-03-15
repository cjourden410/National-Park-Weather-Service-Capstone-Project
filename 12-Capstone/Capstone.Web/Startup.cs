using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TE.AuthLib;
using TE.AuthLib.DAL;

namespace Capstone.Web
{
    public class Startup
    {
        // Add private connection string
        private string connectionString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            // Sets up session for MVC
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Sets session expiration to 20 minuates
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });


            // Define connection string here for default and user
            connectionString = Configuration.GetConnectionString("Default");
            string userConnectionString = Configuration.GetConnectionString("User");

            services.AddTransient<IParkSqlDAO, ParkSqlDAO>((x) => new ParkSqlDAO(connectionString));
            services.AddTransient<ISurveyResultSqlDAO, SurveyResultSqlDAO>((x) => new SurveyResultSqlDAO(connectionString));
            services.AddTransient<IUserDAO>(m => new UserSqlDAO(userConnectionString));

            // Setup dependency injection for the authentication provider
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-3.1

            services.AddHttpContextAccessor();
            services.AddScoped<IAuthProvider, SessionAuthProvider>();

            // Configure the Auth filter to re-direct to account/login
            AuthorizeAttribute.Options = new AuthorizeAttribute.AuthorizeAttributeOptions()
            {
                LoginRedirectAction = "Login",
                LoginRedirectController = "Account"
            };

            // Added anti forgery token
            services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
        
        // TODO: Potentially remove method to create a new Survey
        public SurveyResultSqlDAO MakeNewSurvey(IServiceProvider x)
        {
            return new SurveyResultSqlDAO(connectionString);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            // Session configure

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
