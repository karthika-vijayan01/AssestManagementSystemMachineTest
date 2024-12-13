using System;
using System.Collections.Generic;

namespace AssestManagementSystemMachineTest.Models;

public partial class UserType
{
    public int UtId { get; set; }

    public string? PostDesignation { get; set; }

    public int? RegId { get; set; }

    public virtual ICollection<LoginUser> LoginUsers { get; set; } = new List<LoginUser>();

    public virtual UserRegistration? Reg { get; set; }
}
