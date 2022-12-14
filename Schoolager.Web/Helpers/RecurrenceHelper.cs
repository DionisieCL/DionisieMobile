using Schoolager.Web.Data.Entities;
using System;
using System.Collections.Generic;

namespace Schoolager.Web.Helpers
{
    public class RecurrenceHelper : IRecurrenceHelper
    {
        public string GetHolidaysRecurrenceException(List<Holiday> holidays)
        {
            string recurrence = "";

            List<DateTime> dates = new List<DateTime>();

            foreach (Holiday holiday in holidays)
            {
                dates.AddRange(GetDateRange(holiday.StartTime, holiday.EndTime));
            }

            List<string> dateStrings = GetDateStrings(dates);

            recurrence = string.Join(",", dateStrings);

            return recurrence;
        }

        public string GetDaysBeforeRecurrenceException(DateTime firstDayOfSchool)
        {
            DateTime firstDay = firstDayOfSchool.StartOfWeek(DayOfWeek.Monday);

            List<DateTime> dates = GetDateRange(firstDay, firstDayOfSchool);

            dates.RemoveAt(dates.Count - 1);

            List<string> dateStrings = GetDateStrings(dates);
            
            return string.Join(",", dateStrings);
        }

        public string GetRecurrenceRule(DateTime endDate)
        {
            string ruleBase = "FREQ=WEEKLY;INTERVAL=1;UNTIL=";

            string month = endDate.Month < 10 ? $"0{endDate.Month}" : $"{endDate.Month}";
            string day = endDate.Day < 10 ? $"0{endDate.Day}" : $"{endDate.Day}";

            string dateString = $"{endDate.Year}{month}{day}T190801Z;";

            return ruleBase + dateString;
        }

        public List<Lesson> SetRecurence(List<Lesson> lessons, SchoolYear schoolYear)
        {
            foreach (var lesson in lessons)
            {
                TimeSpan startTime = lesson.StartTime.Value.TimeOfDay;
                TimeSpan endTime = lesson.EndTime.Value.TimeOfDay;
                lesson.RecurrenceRule = GetRecurrenceRule(schoolYear.EndDate);
                lesson.StartTime = schoolYear.StartDate.Add(startTime);
                lesson.EndTime = schoolYear.StartDate.Add(endTime);
            }

            return lessons;
        }

        public List<Lesson> SetRecurrenceExceptions(List<Lesson> lessons, List<Holiday> holidays, SchoolYear schoolYear)
        {
            string exceptions = GetHolidaysRecurrenceException(holidays);

            string exception;
            string daysBefore;

            foreach (var lesson in lessons)
            {
                daysBefore = GetDaysBeforeRecurrenceException(schoolYear.StartDate);
                exception = lesson.RecurrenceException;
                lesson.RecurrenceException = $"{exception},{exceptions},{daysBefore}";
            }

            return lessons;
        }

        private List<DateTime> GetDateRange(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = new List<DateTime>();

            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            return dates;
        }

        private List<string> GetDateStrings(List<DateTime> dates)
        {
            List<string> dateString = new List<string>();

            foreach(DateTime date in dates)
            {
                dateString.Add($"{date.ToString("yyyyMMdd")}T000000Z");
            }

            return dateString;
        }
    }
}
