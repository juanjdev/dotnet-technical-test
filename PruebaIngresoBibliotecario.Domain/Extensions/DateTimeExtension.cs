using System;

namespace PruebaIngresoBibliotecario.Api.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime AddWorkingDays(this DateTime date, int days)
        {
            int direction = 1;
            DateTime newDate = date; 

            while(days > 0)
            {
                newDate = newDate.AddDays(direction);
                if (!IsWeekend(newDate))
                {
                    days -= 1;
                }
            }
            return newDate;
        }

        private static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
