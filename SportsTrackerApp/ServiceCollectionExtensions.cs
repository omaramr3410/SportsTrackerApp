using SportsTrackerApp.Context;
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
        public static void SetupAppBuild(this IServiceCollection services)
        {
            // Add Controller Services to build
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddHttpContextAccessor();

            //services.AddIdentity<UserModel, IdentityRole>()
            //    .AddDefaultTokenProviders();

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            //        options.SlidingExpiration = true;
            //        options.AccessDeniedPath = "/Forbidden/";
            //    });
            services
                .AddAuthentication("cookie")
                .AddCookie("cookie");

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
