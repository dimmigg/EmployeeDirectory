using EmployeeDirectory.DAL.Emtityes;

namespace EmployeeDirectory.ViewModels.Directory
{
    internal class EmployeeViewModel : TreeBase
    {
        private Employee _currentEmployee;
        public Employee CurrentEmployee
        {
            get => _currentEmployee;
            set => Set(ref _currentEmployee, value);
        }

        public EmployeeViewModel(Employee employee)
        {
            CurrentEmployee = employee;
        }
    }
}
