using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Service;
using EmployeeDirectory.Services.Interfaces;
using EmployeeDirectory.ViewModels.Directory;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private Entity _selectedItem;

        public Entity SelectedItem
        {
            get => _selectedItem;
            set
            {
                if(Set(ref _selectedItem, value))
                    ChangeSelectedItem(value);
            }
        }

        private TreeBase _currentPage;
        public TreeBase CurrentPage
        {
            get => _currentPage;
            set => Set(ref _currentPage, value);
        }

        private ICollection<Company> _companies = new List<Company>();
        public ICollection<Company> Companies
        {
            get => _companies;
            set => Set(ref _companies, value);
        }

        private ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> Employees
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
            Employees.AddClear(await _employeeRepo.Items.ToListAsync());
        }

        private ICommand _addItemCommand;
        public ICommand AddItemCommand => _addItemCommand
            ??= new LambdaCommand<string>(OnAddItemCommandExecuted, CanAddItemCommandExecute);

        private bool CanAddItemCommandExecute(string type) => true;

        private async void OnAddItemCommandExecuted(string type)
        {
            var dialog = _userDialog.CreateUserDialog(type);
            if (dialog == null) return;
            if (await dialog.Add())
                await OnLoadDataCommandExecuted();
        }

        private ICommand _editItemCommand;
        public ICommand EditItemCommand => _editItemCommand
            ??= new LambdaCommand<Entity>(OnEditItemCommandExecuted, CanEditItemCommandExecute);

        private bool CanEditItemCommandExecute(Entity entity) => entity != null || SelectedItem != null;

        private async void OnEditItemCommandExecuted(Entity entity)
        {
            var dialog = _userDialog.CreateUserDialog(entity);
            if(dialog == null) return;
            if (await dialog.Edit(entity))
                await OnLoadDataCommandExecuted();
        }

        private ICommand _removeItemCommand;
        public ICommand RemoveItemCommand => _removeItemCommand
            ??= new LambdaCommand<Entity>(OnRemoveItemCommandExecuted, CanRemoveItemCommandExecute);

        private bool CanRemoveItemCommandExecute(Entity entity) => entity != null || SelectedItem != null;

        private async void OnRemoveItemCommandExecuted(Entity entity)
        {
            var itemToRemove = entity ?? SelectedItem;
            var dialog = _userDialog.CreateUserDialog(itemToRemove);
            if (dialog == null) return;
            if (await dialog.Remove(itemToRemove))
                await OnLoadDataCommandExecuted();
            
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
