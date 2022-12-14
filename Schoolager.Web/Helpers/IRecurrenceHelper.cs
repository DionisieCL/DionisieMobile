using Schoolager.Web.Data.Entities;
using System;
using System.Collections.Generic;

namespace Schoolager.Web.Helpers
{
    public interface IRecurrenceHelper
    {
        string GetRecurrenceRule(DateTime endDate);

        string GetHolidaysRecurrenceException(List<Holiday> holidays);

        List<Lesson> SetRecurrenceExceptions(List<Lesson> lessons, List<Holiday> holidays, SchoolYear schoolYear);
        List<Lesson> SetRecurence(List<Lesson> lessons, SchoolYear schoolYear);
    }
}
