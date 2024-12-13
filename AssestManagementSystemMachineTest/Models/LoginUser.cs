using System;
using System.Collections.Generic;

namespace AssestManagementSystemMachineTest.Models;

public partial class LoginUser
{
    public int LId { get; set; }

    public string? UserName { get; set; }

    public string? UserPass { get; set; }

   public int? UtId { get; set; }

    public virtual UserType? Ut { get; set; }
}
