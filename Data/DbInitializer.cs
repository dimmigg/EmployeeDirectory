using EmployeeDirectory.DAL.Context;
using EmployeeDirectory.DAL.Emtityes;
using MathCore;
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
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            _logger.LogInformation("Удаление существующей БД выполнена за {0} мс", timer.ElapsedMilliseconds);


            _logger.LogInformation("Миграция БД...");
            await _db.Database.MigrateAsync();
            _logger.LogInformation("Миграция БД выполнена за {0} мс", timer.ElapsedMilliseconds);

            if (await _db.Companies.AnyAsync()) return;

            await InitializeCompaniesAsync();
            await InitializeDepartmentsAsync();
            await InitializeEmployeesAsync();

            _logger.LogInformation("Инициализация БД выполнена за {0} сек", timer.Elapsed.TotalSeconds);
        }

        private const int _companiesCount = 10;
        private Company[] _companies;
        private async Task InitializeCompaniesAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Компаний...");

            _companies = Enumerable.Range(1, _companiesCount)
                .Select(i => new Company
                {
                    Name = $"Компания {i}",
                    Created = DateTime.Now,
                    Address = $"Адрес {i}"
                })
                .ToArray();

            await _db.Companies.AddRangeAsync(_companies);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация Компаний выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _departmentsCount = 10;
        private Department[] _departments;
        private async Task InitializeDepartmentsAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Отделов...");

            var rnd = new Random();
            _departments = Enumerable.Range(1, _departmentsCount)
                .Select(i => new Department
                {
                    Name = $"Отдел {i}",
                    Company = rnd.NextItem(_companies)
                })
                .ToArray();

            await _db.Departments.AddRangeAsync(_departments);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация Отделов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _employeesCount = 10;
        private Employee[] _employees;
        private async Task InitializeEmployeesAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Сотрудников...");

            var rnd = new Random();
            _employees = Enumerable.Range(1, _employeesCount)
                .Select(i => new Employee
                {
                    Surname = $"Фамилия {i}",
                    FirstName = $"Имя {i}",
                    SecondName = $"Отчество {i}",
                    Birthday = DateTime.Now,
                    EmploymentDate = DateTime.Now,
                    Position = $"Должность {i}",
                    Wage = (decimal)rnd.NextDouble() * 100000 + 50000,
                    Department = rnd.NextItem(_departments)
                })
                .ToArray();

            await _db.Employees.AddRangeAsync(_employees);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация Сотрудников выполнена за {0} мс", timer.ElapsedMilliseconds);
        }
    }
}
