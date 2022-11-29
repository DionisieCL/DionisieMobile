using System;

namespace Schoolager.Web.Helpers
{
    public class RecurrenceHelper : IRecurrenceHelper
    {
        public string GetRecurrenceRule(DateTime endDate)
        {
            string ruleBase = "FREQ=WEEKLY;INTERVAL=1;UNTIL=";

            string month = endDate.Month < 10 ? $"0{endDate.Month}" : $"{endDate.Month}";
            string day = endDate.Day < 10 ? $"0{endDate.Day}" : $"{endDate.Day}";

            string dateString = $"{endDate.Year}{month}{day}T190801Z;";

            return ruleBase + dateString;
        }
    }
}
