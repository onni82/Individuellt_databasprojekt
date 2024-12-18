using System;
using System.Collections.Generic;

namespace Individuellt_databasprojekt.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? HireDate { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();

    public virtual Role Role { get; set; } = null!;
}
