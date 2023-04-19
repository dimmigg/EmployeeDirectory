using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Services.Interfaces;

namespace EmployeeDirectory.Services.UserDialog
{
    class UserDialogCreator : IUserDialogCreator
    {
        private readonly IRepository<Employee> _employeeeRepo;
        private readonly IRepository<Department> _departmentRepo;
        private readonly IRepository<Company> _companyRepo;

        public UserDialogCreator(
            IRepository<Employee> employeeeRepo,
            IRepository<Department> departmentRepo,
            IRepository<Company> companyRepo)
        {
            _employeeeRepo = employeeeRepo;
            _departmentRepo = departmentRepo;
            _companyRepo = companyRepo;
        }
        public IUserDialog CreateUserDialog(Entity entity)
        {
            if (entity is Company company)
                return new UserDialogCompany(_companyRepo);
            else if (entity is Department department)       
                return new UserDialogDepartment(_employeeeRepo, _departmentRepo, _companyRepo);
            else if (entity is Employee employee)
                return new UserDialogEmployee(_employeeeRepo, _departmentRepo);
            else
                return null;
        }

        public IUserDialog CreateUserDialog(string type)
        {
            switch (type)
            {
                case "Company":
                    return new UserDialogCompany(_companyRepo);
                case "Department":
                    return new UserDialogDepartment(_employeeeRepo, _departmentRepo, _companyRepo);
                case "Employee":
                    return new UserDialogEmployee(_employeeeRepo, _departmentRepo);
                default:
                    break;
            }
            return null;
        }
    }
}
