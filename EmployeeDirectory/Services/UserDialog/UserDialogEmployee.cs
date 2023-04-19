using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.ViewModels.Directory.Dialog;
using EmployeeDirectory.Views.DirectoryPages.Dialogs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDirectory.Services.UserDialog
{
    class UserDialogEmployee : UserDialogBase
    {
        private readonly IRepository<Department> _departmentRepo;
        private readonly IRepository<Employee> _employeeRepo;

        public UserDialogEmployee(
            IRepository<Employee> employeeeRepo,
            IRepository<Department> departmentRepo)
        {
            _departmentRepo = departmentRepo;
            _employeeRepo = employeeeRepo;
        }

        public override async Task<bool> Edit(Entity entity)
        {
            if (entity is Employee employee)
            {
                var editModel = new EmployeeEditorViewModel(employee, _departmentRepo.Items.ToArray());

                var companyEditor = new EmployeeEditor
                {
                    DataContext = editModel
                };

                if (companyEditor.ShowDialog() != true) return false;

                employee.Id = editModel.Id;
                employee.Surname = editModel.Surname;
                employee.FirstName = editModel.FirstName;
                employee.SecondName = editModel.SecondName;
                employee.Birthday = editModel.Birthday;
                employee.EmploymentDate = editModel.EmploymentDate;
                employee.Position = editModel.Position;
                employee.Wage = editModel.Wage;
                employee.Department = editModel.Department;
                employee.DepartmentId = editModel.Department.Id;

                await _employeeRepo.UpdateAsync(employee).ConfigureAwait(false);
                return true;
            }
            return false;
        }

        public override async Task<bool> Add()
        {
            var employee = new Employee() { Birthday = DateTime.Today, EmploymentDate = DateTime.Today };
            var addModel = new EmployeeEditorViewModel(employee, _departmentRepo.Items.ToArray());

            var companyEditor = new EmployeeEditor
            {
                DataContext = addModel
            };

            if (companyEditor.ShowDialog() != true) return false;

            employee.Id = addModel.Id;
            employee.Surname = addModel.Surname;
            employee.FirstName = addModel.FirstName;
            employee.SecondName = addModel.SecondName;
            employee.Birthday = addModel.Birthday;
            employee.EmploymentDate = addModel.EmploymentDate;
            employee.Position = addModel.Position;
            employee.Wage = addModel.Wage;
            employee.Department = addModel.Department;
            employee.DepartmentId = addModel.Department.Id;

            await _employeeRepo.AddAsync(employee).ConfigureAwait(false);
            return true;
        }

        public override async Task<bool> Remove(Entity entity)
        {
            if (entity is Employee employee)
            {
                if (ConfirmWarning($"Вы хотите удалить отрудника {employee}?", "Удаление отдела"))
                {
                    await _employeeRepo.RemoveAsync(employee.Id).ConfigureAwait(false);
                    return true;
                }
            }
            return false;
        }
    }
}
