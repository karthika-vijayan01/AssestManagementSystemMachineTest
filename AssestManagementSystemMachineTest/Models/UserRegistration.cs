using System;
using System.Collections.Generic;

namespace AssestManagementSystemMachineTest.Models;

public partial class UserRegistration
{
    public int RegId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? UserAddress { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<UserType> UserTypes { get; set; } = new List<UserType>();
}
