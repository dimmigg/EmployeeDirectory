using MathCore.WPF.ViewModels;

namespace EmployeeDirectory.ViewModels.Directory
{
    internal class TreeBase : ViewModel
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }
    }
}
