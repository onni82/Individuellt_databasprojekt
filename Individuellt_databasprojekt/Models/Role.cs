using System;
using System.Collections.Generic;

namespace Individuellt_databasprojekt.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public int RoleMonthlyPay { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
