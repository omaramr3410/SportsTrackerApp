using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsTrackerApp.Context;
using SportsTrackerApp.Models;
using SportsTrackerApp.Orchestrators;
using SportsTrackerApp.Repository;

namespace SportsTrackerApp
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension to run proper setup of Program build
        /// </summary>
        /// <param name="services">Service collection of Program</param>
        public static void SetupAppBuild(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Controller Services to build
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddHttpContextAccessor();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityCore<UserModel>()
                .AddEntityFrameworkStores<DataContext>()
                .AddApiEndpoints();

            //services.AddIdentity<UserModel, IdentityRole>()
            //    .AddEntityFrameworkStores<DataContext>()
            //    .AddDefaultTokenProviders();

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            //        options.SlidingExpiration = true;
            //        options.AccessDeniedPath = "/Forbidden/";
            //    });
            //services.AddAuthentication(options =>d
            //{
            //    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            //    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            //    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            //})
            //    .AddCookie(IdentityConstants.ApplicationScheme);
            services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddCookie(IdentityConstants.ApplicationScheme);

            services.AddAuthorization();

            //Add required services
            services.AddRequiredServices();
        }

        public static void AddRequiredServices(this IServiceCollection services) {
            services.AddSingleton<IDapperContext, DapperContext>();

            services.AddTransient<ITeamOrchestrator, TeamOrchestrator>();
            services.AddTransient<ITeamRepository, TeamRepository>();
        }
    }
}
