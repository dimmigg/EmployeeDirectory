using EmployeeDirectory.DAL;
using EmployeeDirectory.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDirectory.Data
{
    internal static class DbRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<MainDB>( opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("MSSQL"));
            })
            .AddTransient<DbInitializer>()
            .AddRepositoriesInDB();
    }
}
