using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.ViewModels.Directory.Dialog;
using EmployeeDirectory.Views.DirectoryPages.Dialogs;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDirectory.Services.UserDialog
{
    class UserDialogDepartment : UserDialogBase
    {
        private readonly IRepository<Department> _departmentRepo;
        private readonly IRepository<Company> _companyRepo;
        private readonly IRepository<Employee> _employeeeRepo;

        public UserDialogDepartment(
            IRepository<Employee> employeeeRepo,
            IRepository<Department> departmentRepo,
            IRepository<Company> companyRepo)
        {
            _departmentRepo = departmentRepo;
            _companyRepo = companyRepo;
            _employeeeRepo = employeeeRepo;
        }

        public override async Task<bool> Edit(Entity entity)
        {
            if (entity is Department department)
            {
                var editModel = new DepartmentEditorViewModel(department, _companyRepo.Items.ToArray(), _employeeeRepo.Items.ToArray());

                var companyEditor = new DepartmentEditor
                {
                    DataContext = editModel
                };

                if (companyEditor.ShowDialog() != true) return false;

                department.Name = editModel.Name;
                department.Id = editModel.Id;
                department.Director = editModel.Director;
                department.DirectorId = editModel.Director?.Id ?? 0;
                department.Company = editModel.Company;
                department.CompanyId = editModel.Company.Id;
                department.Employees = editModel.Employees;

                await _departmentRepo.UpdateAsync(department).ConfigureAwait(false);
                return true;
            }
            return false;
        }

        public override async Task<bool> Add()
        {
            var department = new Department();
            var addModel = new DepartmentEditorViewModel(department, _companyRepo.Items.ToArray(), _employeeeRepo.Items.ToArray());

            var departmentEditor = new DepartmentEditor
            {
                DataContext = addModel
            };

            if (departmentEditor.ShowDialog() != true) return false;
            department.Name = addModel.Name ?? "";
            department.Id = addModel.Id;
            department.Director = addModel.Director;
            department.Company = addModel.Company;
            department.Employees = addModel.Employees;

            await _departmentRepo.AddAsync(department).ConfigureAwait(false); ;
            return true;
        }

        public override async Task<bool> Remove(Entity entity)
        {
            if (entity is Department department)
            {
                if (ConfirmWarning($"Вы хотите удалить отдел {department.Name}?", "Удаление отдела"))
                {
                    await _departmentRepo.RemoveAsync(department.Id).ConfigureAwait(false);
                    return true;
                }
            }
            return false;
        }
    }
}
