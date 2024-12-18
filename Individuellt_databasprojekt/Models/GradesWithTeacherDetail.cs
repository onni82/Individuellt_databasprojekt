using System;
using System.Collections.Generic;

namespace Individuellt_databasprojekt.Models;

public partial class GradesWithTeacherDetail
{
    public string StudentName { get; set; } = null!;

    public string CourseName { get; set; } = null!;

    public string TeacherName { get; set; } = null!;

    public string? Grade { get; set; }

    public DateTime? GradeDate { get; set; }
}
