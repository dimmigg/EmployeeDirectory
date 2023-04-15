using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.Interfaces;
using MathCore.WPF.ViewModels;
using System.Linq;

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
        

        public MainWindowViewModel(IRepository<Department> departmentRepo, IRepository<Company> companyRepository)
        {
            _departmentRepo = departmentRepo;
            _companyRepository = companyRepository;
            var a = _companyRepository.Items;
        }
    }
}
