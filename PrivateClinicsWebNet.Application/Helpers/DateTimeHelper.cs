using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime TryParseDateTime(string date, string time)
        {
            DateTime parsedDateTime;
            if (DateTime.TryParse($"{date} {time}", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
                return parsedDateTime;
            else
                return DateTime.MinValue;
        }
    }
}
