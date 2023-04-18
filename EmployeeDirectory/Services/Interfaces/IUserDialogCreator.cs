using EmployeeDirectory.DAL.Emtityes.Base;

namespace EmployeeDirectory.Services.Interfaces
{
    interface IUserDialogCreator
    {
        IUserDialog CreateUserDialog(Entity entity);
    }
}
