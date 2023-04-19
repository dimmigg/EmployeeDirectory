using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Service;
using EmployeeDirectory.Services.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using static System.Reflection.Metadata.BlobBuilder;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeDirectory.ViewModels.Reports
{
    internal class SalaryReportViewModel : ViewModel
    {
        private readonly IRepository<Company> _companyRepo;

        private ObservableCollection<Company> _companies = new ObservableCollection<Company>();

        private ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);
        private bool CanLoadDataCommandExecute() => true;
        private async Task OnLoadDataCommandExecuted()
        {
            Companies.AddClear(await _companyRepo.Items.ToArrayAsync());
        }


        public ObservableCollection<Company> Companies
        {
            get => _companies;
            set => Set(ref _companies, value);
        }

        public SalaryReportViewModel(IRepository<Company> companyRepo)
        {
            _companyRepo = companyRepo;
        }
    }
}
