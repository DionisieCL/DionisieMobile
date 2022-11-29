using System;

namespace Schoolager.Web.Helpers
{
    public interface IRecurrenceHelper
    {
        string GetRecurrenceRule(DateTime endDate);
    }
}
