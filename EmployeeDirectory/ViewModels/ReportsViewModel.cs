using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeDirectory.ViewModels
{
    internal class ReportsViewModel : ViewModel
    {
        private ICommand _computeReportsCommand;
        public ICommand ComputeReportsCommand => _computeReportsCommand ??=
            new LambdaCommandAsync(OnComputeReportsExecuted, CanComputeReportsExecute);

        private bool CanComputeReportsExecute(object? arg) => true;

        private async Task OnComputeReportsExecuted(object? obj)
        {
        }
    }
}
