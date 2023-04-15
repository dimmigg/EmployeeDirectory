using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDirectory.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModel(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>();
    }
}
