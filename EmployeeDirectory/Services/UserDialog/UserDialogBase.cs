using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.Services.Interfaces;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeDirectory.Services.UserDialog
{
    abstract class UserDialogBase : IUserDialog
    {
        public abstract Task<bool> Remove(Entity entity);
        public abstract Task<bool> Edit(Entity entity);
        public abstract Task<bool> Add();

        public bool ConfirmInformation(string Information, string Caption) => MessageBox
           .Show(
                Information, Caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Information)
                == MessageBoxResult.Yes;

        public bool ConfirmWarning(string Warning, string Caption) => MessageBox
           .Show(
                Warning, Caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning)
                == MessageBoxResult.Yes;

        public bool ConfirmError(string Error, string Caption) => MessageBox
           .Show(
                Error, Caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Error)
                == MessageBoxResult.Yes;
    }
}
