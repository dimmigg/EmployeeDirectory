using MathCore.WPF.ViewModels;

namespace EmployeeDirectory.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public string _title = "Главное окно программы";
        public string Title 
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
