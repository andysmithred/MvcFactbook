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
            return date.ToString("dd MMM yyyy");
        }

        public static string GetDateLabel(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return GetDateLabel(startDate) + " to " + GetDateLabel(endDate);
            }
            else if (startDate.HasValue)
            {
                // Have a start date but if we are here then no end date
                return GetDateLabel(startDate) + " to ...";
            }
            else if (endDate.HasValue)
            {
                // Have an enddate but no start date.
                return "? to " + GetDateLabel(endDate);
            }
            else
            {
                // If we are here then no start or end date.
                return "--";
            }
        }

        public static TimeSpan? GetTimepan(DateTime? date)
        {
            return GetTimepan(date, DateTime.Now);
        }

        public static TimeSpan? GetTimepan(DateTime? start, DateTime? end)
        {
            if(start.HasValue && end.HasValue)
            {
                //Have both a start and end date
                return end.Value - start.Value;
            }
            else if(start.HasValue)
            {
                //Have a start but not an end. Use now.
                return DateTime.Now - start.Value;
            }
            else
            {
                //Have an end date but no start date, or no start or end date, return null
                return null;
            }
        }

        public static string Format(TimeSpan? timeSpan)
        {
            if(timeSpan.HasValue)
            {
                return Format(timeSpan.Value);
            }
            else
            {
                return "--";
            }
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
