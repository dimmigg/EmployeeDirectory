using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;

namespace EmployeeDirectory.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public string _title = "Главное окно программы";
        private readonly IRepository<Department> _departmentRepo;
        private readonly IRepository<Company> _companyRepository;

        public string Title 
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private ViewModel _currentModel;
        public ViewModel CurrentModel
        {
            get => _currentModel;
            set => Set(ref _currentModel, value);
        }

        private ICommand _showDirectoryViewCommand;
        public ICommand ShowDirectoryViewCommand => _showDirectoryViewCommand ??=
            new LambdaCommand(OnShowDirectoryCommandExecuted, CanShowDirectoryCommandExecute);

        private bool CanShowDirectoryCommandExecute(object? arg) => true;

        private void OnShowDirectoryCommandExecuted(object? obj)
        {
            CurrentModel = new DirectoryViewModel(_departmentRepo, _companyRepository);
        }

        private ICommand _showReportsViewCommand;
        public ICommand ShowReportsViewCommand => _showReportsViewCommand ??=
            new LambdaCommand(OnShowReportsCommandExecuted, CanShowReportsCommandExecute);

        private bool CanShowReportsCommandExecute(object? arg) => true;

        private void OnShowReportsCommandExecuted(object? obj)
        {
            CurrentModel = new ReportsViewModel();
        }

        public MainWindowViewModel(IRepository<Department> departmentRepo, IRepository<Company> companyRepository)
        {
            _departmentRepo = departmentRepo;
            _companyRepository = companyRepository;
            var a = _departmentRepo.Items;
        }
    }
}
