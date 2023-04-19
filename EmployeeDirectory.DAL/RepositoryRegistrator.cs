using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDirectory.DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
            .AddTransient<IRepository<Company>, CompaniesRepository>()
            .AddTransient<IRepository<Department>, DepartamentsRepository>()
            .AddTransient<IRepository<Employee>, EmployeesRepository>();
    }
}
