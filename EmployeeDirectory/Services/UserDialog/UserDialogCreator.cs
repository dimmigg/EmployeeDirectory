using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Services.Interfaces;
using EmployeeDirectory.ViewModels.Directory;

namespace EmployeeDirectory.Services.UserDialog
{
    class UserDialogCreator : IUserDialogCreator
    {
        private readonly IRepository<Company> _companyRepo;

        public UserDialogCreator(IRepository<Company> companyRepo)
        {
            _companyRepo = companyRepo;
        }
        public IUserDialog CreateUserDialog(Entity entity)
        {
            if (entity is Company company)
                return new UserDialogCompany(_companyRepo);
            //else if (value is Department department)
            //{
            //    department.Director = Employees.FirstOrDefault(e => e.Id == department.DirectorId);
            //    CurrentPage = new DepartmentViewModel(department);
            //}
            //else if (value is Employee employee)
            //    CurrentPage = new EmployeeViewModel(employee);
            else
                return null;
        }
    }
}
