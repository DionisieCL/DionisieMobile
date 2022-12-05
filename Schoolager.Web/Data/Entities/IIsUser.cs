using System;

namespace Schoolager.Web.Data.Entities
{
    public interface IIsUser
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string PhoneNumber { get; set; }

        Guid ImageId { get; set; }

        string Email { get; set; }
    }
}
