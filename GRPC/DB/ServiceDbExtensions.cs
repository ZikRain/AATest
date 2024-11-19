using GRPC.DB.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GRPC.DB
{
    public static class ServiceDbExtensions
    {
        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestDbContext>(opt => BuildOptions(opt, configuration.GetConnectionString("TestDbConnection")!));
        }

        private static DbContextOptions BuildOptions(DbContextOptionsBuilder options, string connection)
        {
            options.UseNpgsql(connection, serverOpt =>
            {

            });

            options.EnableSensitiveDataLogging();
            return options.Options;
        }
    }
}
