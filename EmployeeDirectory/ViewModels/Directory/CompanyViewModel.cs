using EmployeeDirectory.DAL.Emtityes;

namespace EmployeeDirectory.ViewModels.Directory
{
    internal class CompanyViewModel : TreeBase
    {
        private Company _currentCompany;
        public Company CurrentCompany
        {
            get => _currentCompany;
            set => Set(ref _currentCompany, value);
        }

        public CompanyViewModel(Company company)
        {
            CurrentCompany = company;
        }
    }
}
