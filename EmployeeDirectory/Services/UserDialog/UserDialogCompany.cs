using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.ViewModels.Directory.Dialog;
using EmployeeDirectory.Views.DirectoryPages.Dialogs;
using System.Threading.Tasks;

namespace EmployeeDirectory.Services.UserDialog
{
    class UserDialogCompany : UserDialogBase
    {
        private readonly IRepository<Company> _companyRepo;

        public UserDialogCompany(IRepository<Company> companyRepo)
        {
            _companyRepo = companyRepo;
        }

        public override async Task<bool> Edit(Entity entity)
        {
            if (entity is Company company)
            {
                var editModel = new CompanyEditorViewModel(company);

                var companyEditor = new CompanyEditor
                {
                    DataContext = editModel
                };

                if (companyEditor.ShowDialog() != true) return false;

                company.Name = editModel.Name;
                company.Id = editModel.Id;
                company.Address = editModel.Address;
                company.Created = editModel.Created;
                company.Departments = editModel.Departments;

                await _companyRepo.UpdateAsync(company).ConfigureAwait(false);
                return true;
            }
            return false;
        }

        public override bool Add()
        {
            var company = new Company();
            var addModel = new CompanyEditorViewModel(company);

            var companyEditor = new CompanyEditor
            {
                DataContext = addModel
            };

            if (companyEditor.ShowDialog() != true) return false;

            company.Name = addModel.Name;
            company.Name = addModel.Name;
            company.Address = addModel.Address;
            company.Created = addModel.Created;
            company.Departments = addModel.Departments;

            _companyRepo.Update(company);
            return true;
        }
    }
}
