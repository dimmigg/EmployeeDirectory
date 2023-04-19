using EmployeeDirectory.DAL.Emtityes.Base;
using System.Threading.Tasks;

namespace EmployeeDirectory.Services.Interfaces
{
    internal interface IUserDialog
    {
        Task<bool> Remove(Entity entity);
        Task<bool> Edit(Entity entity);
        Task<bool> Add();
        bool ConfirmInformation(string Information, string Caption);
        bool ConfirmWarning(string Warning, string Caption);
        bool ConfirmError(string Error, string Caption);
    }
}
