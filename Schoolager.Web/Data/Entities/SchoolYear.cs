using System;

namespace Schoolager.Web.Data.Entities
{
    public class SchoolYear : IEntity
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
