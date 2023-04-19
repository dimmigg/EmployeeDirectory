using EmployeeDirectory.DAL.Emtityes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace EmployeeDirectory.Converters
{
    [ValueConversion(typeof(HashSet<Department>), typeof(decimal))]
    public class SalaryCompanyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HashSet<Department> departments)
            {
                decimal result = 0;
                foreach (var department in departments)
                {
                    foreach (var employee in department.Employees)
                    {
                        result += employee.Wage;
                    }
                }
                return result;
            }
            else 
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
