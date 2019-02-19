using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Classes
{
    public static class CommonFunctions
    {
        private const decimal DaysInAYear = 365.242M;

        public static string GetDateLabel(DateTime? date)
        {
            return date.HasValue ? GetDateLabel(date.Value) : "--";
        }

        public static string GetDateLabel(DateTime date)
        {
            return date.ToString("dd MMMM yyyy");
        }

        public static string Format(TimeSpan timeSpan)
        {
            var builder = new StringBuilder();

            var totalDays = (decimal)timeSpan.TotalDays;
            var totalYears = totalDays / DaysInAYear;
            var years = (int)Math.Floor(totalYears);

            totalDays -= (years * DaysInAYear);
            var days = (int)Math.Floor(totalDays);

            Func<int, string> sIfPlural = value =>
                value > 1 ? "s" : string.Empty;

            if (years > 0)
            {
                builder.AppendFormat(
                    CultureInfo.InvariantCulture,
                    "{0} year{1}",
                    years,
                    sIfPlural(years));

                if (days > 0)
                {
                    builder.Append(" ");
                }
            }

            if (days > 0)
            {
                builder.AppendFormat(
                    CultureInfo.InvariantCulture,
                    "{0} day{1}",
                    days,
                    sIfPlural(days));
            }

            var length = builder.ToString();
            return length;

        }

    }
}
