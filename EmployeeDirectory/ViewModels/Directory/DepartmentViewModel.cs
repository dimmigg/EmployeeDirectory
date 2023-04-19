using EmployeeDirectory.DAL.Emtityes;

namespace EmployeeDirectory.ViewModels.Directory
{
    internal class DepartmentViewModel : TreeBase
    {
        private Department _currentDepartment;
        public Department CurrentDepartment
        {
            get => _currentDepartment;
            set => Set(ref _currentDepartment, value);
        }

        public DepartmentViewModel(Department department)
        {
            CurrentDepartment = department;
        }
    }
}
