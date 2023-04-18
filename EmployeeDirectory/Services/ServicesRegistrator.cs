using EmployeeDirectory.Services.Interfaces;
using EmployeeDirectory.Services.UserDialog;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDirectory.Services
{
    internal static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IUserDialogCreator, UserDialogCreator>()
            .AddTransient<IUserDialog, UserDialogCompany>();
    }
}
