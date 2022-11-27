using System.Collections.Generic;

namespace Schoolager.Web.Constants
{
    public static class Holidays
    {
        private static List<string> Days = new List<string>
        {
            "0101",
            "0425",
            "0501",
            "0610",
            "0815",
            "1005",
            "1101",
            "1201",
            "1208",
            "1225",
        };

        public static string GetStaticHolidays()
        {
            List<string> holidays = new List<string>();
            for (int i = 2022; i < 2050; i++)
            {
                foreach(string day in Days)
                {
                    holidays.Add($"{i}{day}T000000Z");
                };
            }

            return string.Join(",", holidays);
        }
    }
}
