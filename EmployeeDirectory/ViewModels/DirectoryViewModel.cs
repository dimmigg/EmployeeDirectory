using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.Interfaces;
using MathCore.WPF.ViewModels;

namespace EmployeeDirectory.ViewModels
{
    internal class DirectoryViewModel : ViewModel
    {
        private readonly IRepository<Department> _departmentRepo;
        private readonly IRepository<Company> _companyRepository;

        public DirectoryViewModel(IRepository<Department> departmentRepo, IRepository<Company> companyRepository)
        {
            _departmentRepo = departmentRepo;
            _companyRepository = companyRepository;
        }
    }
}
