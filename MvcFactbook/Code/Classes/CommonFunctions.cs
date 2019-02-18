using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFactbook.Code.Classes
{
    public static class CommonFunctions
    {
        public static string GetDateLabel(DateTime? date)
        {
            return date.HasValue ? GetDateLabel(date.Value) : "--";
        }

        public static string GetDateLabel(DateTime date)
        {
            return date.ToString("dd MMMM yyyy");
        }

    }
}
