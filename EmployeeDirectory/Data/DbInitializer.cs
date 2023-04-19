using EmployeeDirectory.DAL.Context;
using EmployeeDirectory.DAL.Emtityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDirectory.Data
{
    internal class DbInitializer
    {
        private readonly MainDB _db;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(MainDB db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация БД...");

            _logger.LogInformation("Удаление существующей БД...");
            //await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            _logger.LogInformation("Удаление существующей БД выполнена за {0} мс", timer.ElapsedMilliseconds);


            _logger.LogInformation("Миграция БД...");
            await _db.Database.MigrateAsync();
            _logger.LogInformation("Миграция БД выполнена за {0} мс", timer.ElapsedMilliseconds);

            if (await _db.Companies.AnyAsync()) return;

            await InitializeCompaniesAsync();
            await InitializeDepartmentsAsync();
            await InitializeEmployeesAsync();
            await UpdateDirectorsAsync();

            _logger.LogInformation("Инициализация БД выполнена за {0} сек", timer.Elapsed.TotalSeconds);
        }

        private const int _companiesCount = 3;
        private Company[] _companies;
        private async Task InitializeCompaniesAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Компаний...");

            _companies = Enumerable.Range(1, _companiesCount)
                .Select(i => new Company
                {
                    Name = $"Компания {i}",
                    Created = GetRandomDate(),
                    Address = $"Адрес {i}"
                })
                .ToArray();

            await _db.Companies.AddRangeAsync(_companies);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация Компаний выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private DateTime GetRandomDate()
        {
            var gen = new Random();
            var start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        private const int _departmentsCount = 3;
        private Department[] _departments;
        private async Task InitializeDepartmentsAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Отделов...");

            foreach (var company in _companies)
            {
                _departments = Enumerable.Range(1, _departmentsCount)
                .Select(i => new Department
                {
                    Name = $"{company.Name} - Отдел  {i}",
                    Company = company
                })
                .ToArray();

                await _db.Departments.AddRangeAsync(_departments);
            }
            
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация Отделов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _employeesCount = 2;
        private Employee[] _employees;
        private async Task InitializeEmployeesAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Сотрудников...");

            var rnd = new Random();
            foreach (var department in _db.Departments)
            {
                _employees = Enumerable.Range(1, _employeesCount)
               .Select(i => new Employee
               {
                   Surname = $"Фамилия {i}",
                   FirstName = $"Имя {i}",
                   SecondName = $"Отчество {i}",
                   Birthday = GetRandomDate(),
                   EmploymentDate = GetRandomDate(),
                   Position = $"{department.Name} Должность {i}",
                   Wage = (decimal)rnd.NextDouble() * 100000 + 50000,
                   Department = department
               })
               .ToArray();
                await _db.Employees.AddRangeAsync(_employees);
            }
           
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация Сотрудников выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private async Task UpdateDirectorsAsync()
        {
            var rnd = new Random();
            foreach (var department in _db.Departments)
            {              
                var dir = rnd.NextItem(await _db.Employees.Where(e => e.Department == department).FirstOrDefaultAsync());
                department.Director = dir;
                department.DirectorId = dir.Id;

                _db.Departments.Update(department);
                _db.Entry(department).State = EntityState.Modified;
            }
            await _db.SaveChangesAsync();
        }
    }
}
