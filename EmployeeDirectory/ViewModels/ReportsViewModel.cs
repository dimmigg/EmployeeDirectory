using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.ViewModels.Reports;
using MathCore.WPF.ViewModels;

namespace EmployeeDirectory.ViewModels
{
    internal class ReportsViewModel : ViewModel
    {
        private readonly IRepository<Department> _departmentRepo;
        private readonly IRepository<Company> _companyRepo;
        private readonly IRepository<Employee> _employeeRepo;

        private SalaryReportViewModel _salaryReport;
        public SalaryReportViewModel SalaryReport
        {
            get => _salaryReport;
            set => Set(ref _salaryReport, value);
        }

        private EmployeesListViewModel _employeesList;
        public EmployeesListViewModel EmployeesList
        {
            get => _employeesList;
            set => Set(ref _employeesList, value);
        }

        public ReportsViewModel(
            IRepository<Department> departmentRepo,
            IRepository<Company> companyRepo,
            IRepository<Employee> employeeRepo)
        {
            _departmentRepo = departmentRepo;
            _companyRepo = companyRepo;
            _employeeRepo = employeeRepo;

            SalaryReport = new SalaryReportViewModel(_companyRepo);
            EmployeesList = new EmployeesListViewModel(_companyRepo);
        }
    }
}
