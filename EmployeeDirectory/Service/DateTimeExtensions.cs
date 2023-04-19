namespace System
{
    public static class DateTimeExtensions
    {
        public static int GetDifferenceInYears(this DateTime date1, DateTime date2)
        {
            int result = (date1.Year - date2.Year);
            if (date1.DayOfYear < date2.DayOfYear)
                result--;

            return result;
        }
    }
}
