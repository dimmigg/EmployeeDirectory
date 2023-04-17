using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.ViewModels.Directory;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmployeeDirectory.ViewModels
{
    internal class DirectoryViewModel : ViewModel
    {
        private readonly IRepository<Department> _departmentRepo;
        private readonly IRepository<Company> _companyRepository;

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

        private List<Company> _companies;
        public List<Company> Companies
        {
            get => _companies;
            set => Set(ref _companies, value);
        }

        public DirectoryViewModel(IRepository<Department> departmentRepo, IRepository<Company> companyRepository)
        {
            _departmentRepo = departmentRepo;
            _companyRepository = companyRepository;
            Companies = _companyRepository.Items.ToList();
            //Companies = _companyRepository.Items.Select(c => new CompanyViewModel(c)).ToList();
        }

        private void ChangeSelectedItem(Entity value)
        {
            IsChoiceItem = true;
            if (value is Company company)
                CurrentPage = new CompanyViewModel(company);
            else if (value is Department department)
                CurrentPage = new DepartmentViewModel(department);
            else if (value is Employee employee)
                CurrentPage = new EmployeeViewModel(employee);
            else
                IsChoiceItem = false;
        }
    }
}
