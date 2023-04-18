using EmployeeDirectory.DAL.Emtityes.Base;
using System.Threading.Tasks;

namespace EmployeeDirectory.Services.Interfaces
{
    internal interface IUserDialog
    {
        Task<bool> Edit(Entity entity);
        bool Add();

        bool ConfirmInformation(string Information, string Caption);
        bool ConfirmWarning(string Warning, string Caption);
        bool ConfirmError(string Error, string Caption);
    }
}
