using System;

namespace DABTechs.eCommerce.Sales.Common
{
    public static class Dates
    {
        /// <summary>This will Format Date according to the User Input string</summary>
        /// <param name="value">Actual Date value</param>
        /// <param name="requiredFormat">Required format type</param>
        /// <returns>Formatted Date String</returns>
        public static DateTime? FormatToDate(string value)
        {
            if (DateTime.TryParse(value, out DateTime priceDate))
            {
                return priceDate;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the date time from unix date format.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static DateTime? GetDateTimeFromUnixFormat(double? input)
        {
            if (!input.HasValue)
            {
                return null;
            }

            var unixZero = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return unixZero.AddSeconds(input.Value);
        }

        /// <summary>
        /// Converts the UTC date to GMT.
        /// </summary>
        /// <param name="utcDate">The UTC date.</param>
        /// <returns></returns>
        public static DateTime? ConvertUTCDateToGMT(DateTime? utcDate)
        {
            try
            {
                if (utcDate == null) { return null; }
                DateTime gmtDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(utcDate.Value, "UTC", "GMT Standard Time");
                return gmtDate;
            }
            catch
            {
                return null;
            }
        }
    }
}