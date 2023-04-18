using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDirectory.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
