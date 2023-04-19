using EmployeeDirectory.DAL.Emtityes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace EmployeeDirectory.Converters
{
    [ValueConversion(typeof(HashSet<Employee>), typeof(decimal))]
    public class SalaryDepartmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HashSet<Employee> employees)
            {
                decimal result = 0;
                    foreach (var employee in employees)
                    {
                        result += employee.Wage;
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
