using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Services.Interfaces;
using EmployeeDirectory.Services.UserDialog;
using EmployeeDirectory.ViewModels.Directory;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeDirectory.ViewModels
{
    internal class DirectoryViewModel : ViewModel
    {
        private readonly IRepository<Department> _departmentRepo;
        private readonly IRepository<Company> _companyRepo;
        private readonly IRepository<Employee> _employeeRepo;
        private readonly IUserDialogCreator _userDialog;
        private bool _isChoiceItem;
        public bool IsChoiceItem
        {
            get => _isChoiceItem;
            set => Set(ref _isChoiceItem, value);
        }

        private Entity selectedItem;

        public Entity SelectedItem
        {
            get => selectedItem;
            set
            {
                ChangeSelectedItem(value);
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private TreeBase _currentPage;
        public TreeBase CurrentPage
        {
            get => _currentPage;
            set => Set(ref _currentPage, value);
        }

        private IList<Company> _companies;
        public IList<Company> Companies
        {
            get => _companies;
            set => Set(ref _companies, value);
        }

        private IList<Employee> _employees;
        public IList<Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value);
        }

        private ICommand _loadDataCommand;

        public ICommand LoadDataCommand => _loadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);

        private bool CanLoadDataCommandExecute() => true;

        private async Task OnLoadDataCommandExecuted()
        {
            Companies = await _companyRepo.Items.ToListAsync();
            Employees = await _employeeRepo.Items.ToListAsync();
        }

        private ICommand _addItemCommand;
        public ICommand AddItemCommand => _addItemCommand
            ??= new LambdaCommandAsync(OnAddItemCommandExecuted, CanAddItemCommandExecute);

        private bool CanAddItemCommandExecute() => IsChoiceItem;

        private async Task OnAddItemCommandExecuted()
        {
            
        }

        private ICommand _editItemCommand;
        public ICommand EditItemCommand => _editItemCommand
            ??= new LambdaCommand<Entity>(OnEditItemCommandExecuted, CanEditItemCommandExecute);

        private bool CanEditItemCommandExecute(Entity entity) => entity != null || SelectedItem != null;

        private async void OnEditItemCommandExecuted(Entity entity)
        {
            var dialog = _userDialog.CreateUserDialog(entity);
            if(dialog == null) return;
            if(await dialog.Edit(entity))
                await OnLoadDataCommandExecuted();
        }

        private ICommand _removeItemCommand;
        public ICommand RemoveItemCommand => _removeItemCommand
            ??= new LambdaCommand<Entity>(OnRemoveItemCommandExecuted, CanRemoveItemCommandExecute);

        private bool CanRemoveItemCommandExecute(Entity entity) => entity != null || SelectedItem != null;

        private void OnRemoveItemCommandExecuted(Entity entity)
        {
            var itemToRemove = entity ?? SelectedItem;
        }

        public DirectoryViewModel(
            IRepository<Department> departmentRepo,
            IRepository<Company> companyRepo,
            IRepository<Employee> employeeRepo,
            IUserDialogCreator userDialog)
        {
            _departmentRepo = departmentRepo;
            _companyRepo = companyRepo;
            _employeeRepo = employeeRepo;
            _userDialog = userDialog;
        }

        private async void ChangeSelectedItem(Entity value)
        {
            IsChoiceItem = true;
            if (value is Company company)
                CurrentPage = new CompanyViewModel(company);
            else if (value is Department department)
            {
                department.Director = Employees.FirstOrDefault(e => e.Id == department.DirectorId);
                CurrentPage = new DepartmentViewModel(department);
            }
            else if (value is Employee employee)
                CurrentPage = new EmployeeViewModel(employee);
            else
                IsChoiceItem = false;
        }
    }
}
