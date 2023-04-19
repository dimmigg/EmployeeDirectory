using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Service;
using EmployeeDirectory.Services.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeDirectory.ViewModels.Reports
{
    internal class EmployeesListViewModel : ViewModel
    {
        private readonly IRepository<Company> _companyRepo;

        private ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);
        private bool CanLoadDataCommandExecute() => true;
        private async Task OnLoadDataCommandExecuted()
        {
            Companies.AddClear(await _companyRepo.Items.ToArrayAsync());
            SelectedType = Types[0];
            SelectedCompany = Companies[0];
            ChageDataSource();
        }

        private ObservableCollection<Company> _companies = new ObservableCollection<Company>();
        public ObservableCollection<Company> Companies
        {
            get => _companies;
            set => Set(ref _companies, value);
        }

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                if (Set(ref _selectedCompany, value))
                    ChageDataSource();
            }
        }

        private string[] _types = { "Возраст", "Год рождения" };
        public string[] Types
        {
            get => _types;
            set => Set(ref _types, value);
        }

        private string _selectedType;
        public string SelectedType
        {
            get => _selectedType;
            set
            {
                if (Set(ref _selectedType, value))
                    ChangeType();
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (Set(ref _selectedYear, value))
                    ChageDataSource();
            }
        }

        private ObservableCollection<int> _years = new ObservableCollection<int>();
        public ObservableCollection<int> Years
        {
            get => _years;
            set => Set(ref _years, value);
        }
        private ObservableCollection<SourceData> _sourceData = new ObservableCollection<SourceData>();
        public ObservableCollection<SourceData> SourceData
        {
            get => _sourceData;
            set => Set(ref _sourceData, value);
        }

        public EmployeesListViewModel(IRepository<Company> companyRepo)
        {
            _companyRepo = companyRepo;            
        }

        private void ChangeType()
        {
            switch (SelectedType)
            {
                case "Возраст":
                    ChangeToAge();
                    break;
                case "Год рождения":
                    ChangeToYear();
                    break;
                default:
                    return;
            }
            SelectedYear = Years[0];
        }

        private void ChangeToYear()
        {
            var maxYear = DateTime.Today.Year - 1899;
            Years.AddClear(Enumerable.Range(1900, maxYear).OrderByDescending(x => x));
        }

        private void ChangeToAge()
        {
            Years.AddClear(Enumerable.Range(0, 100).OrderBy(x => x));
        }

        private void ChageDataSource()
        {
            SourceData.Clear();
            var companies = Companies.Where(c => c == SelectedCompany);
            foreach (var company in companies)
            {
                foreach (var department in company.Departments)
                {
                    var employees = GetEmployees(department.Employees);
                    foreach (var employee in employees)
                    {
                        var empl = new SourceData()
                        {
                            Company = company.Name,
                            Department = department.Name,
                            Employee = employee.ToString(),
                            Experience = DateTime.Today.GetDifferenceInYears(employee.EmploymentDate),
                            Age = DateTime.Today.GetDifferenceInYears(employee.Birthday)
                        };
                        SourceData.Add(empl);
                    }

                }
            }
        }

        private IEnumerable<Employee> GetEmployees(ICollection<Employee> employees)
        {
            switch (SelectedType)
            {
                case "Возраст":
                    return employees.Where(x => DateTime.Today.GetDifferenceInYears(x.Birthday) == SelectedYear);
                case "Год рождения":
                    return employees.Where(x => x.Birthday.Year == SelectedYear);
                default:
                    return new List<Employee>();
            }
        }
    }
}
