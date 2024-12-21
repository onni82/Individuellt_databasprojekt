using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Individuellt_databasprojekt.Data;
using Individuellt_databasprojekt.Models;
using Microsoft.EntityFrameworkCore;

namespace Individuellt_databasprojekt
{
    public class SQLMenu
    {
        public static string connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=School;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public static void Run()
        {
            // A simple navigation in the program that allows the user to choose between the following functions.
            while (true)
            {
                Console.WriteLine("Want do you want to do?");
                Console.WriteLine("1. Display staff");
                Console.WriteLine("2. Display all students");
                Console.WriteLine("3. Display all students in a course");
                Console.WriteLine("4. Display all courses");
                Console.WriteLine("5. Display all grades set the last month");
                Console.WriteLine("6. Display all grades for a course");
                Console.WriteLine("7. Add a new student");
                Console.WriteLine("8. Add new staff");
                Console.WriteLine("9. Exit application");

                string? choice = Console.ReadLine();

                // A switch case that handles the user's choice
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("");
                        DisplayStaff();
                        Console.WriteLine("");
                        break;
                    case "2":
                        Console.WriteLine("");
                        DisplayStudents();
                        Console.WriteLine("");
                        break;
                    case "3":
                        Console.WriteLine("");
                        DisplayStudentsInCourse();
                        Console.WriteLine("");
                        break;
                    case "4":
                        Console.WriteLine("");
                        DisplayAllCourses();
                        Console.WriteLine("");
                        break;
                    case "5":
                        Console.WriteLine("");
                        DisplayGradesFromLastMonth();
                        Console.WriteLine("");
                        break;
                    case "6":
                        Console.WriteLine("");
                        DisplayGradesFromCourse();
                        Console.WriteLine("");
                        break;
                    case "7":
                        Console.WriteLine("");
                        AddStudent();
                        Console.WriteLine("");
                        break;
                    case "8":
                        Console.WriteLine("");
                        AddStaff();
                        Console.WriteLine("");
                        break;
                    case "9":
                        // Exits the application
                        return;
                    default:
                        Console.WriteLine("");
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        public static void DisplayStaffRoles()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL query to get all roles from the database
                string query = "SELECT RoleId, RoleName FROM Roles";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("All roles:");

                    // Read the roles from the SQL query result from the database
                    while (reader.Read())
                    {
                        int roleId = reader.GetInt32(0);
                        string roleName = reader.GetString(1);
                        Console.WriteLine($"{roleId}. {roleName}");
                    }
                }
            }
        }

        public static void DisplayStaff()
        {
            DisplayStaffRoles();
            // Display staff
            // The user can choose whether he wants to see all employees, or only within one of the categories, such as teachers
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine("Which role do you want to see? Type a corresponding number, or 0 to view all staff.");
                string? roleChoice = Console.ReadLine();

                if (roleChoice == "0")
                {
                    // Query to get all staff and group by role name
                    string query = @"
                    SELECT r.RoleName, s.FirstName, s.LastName
                    FROM Staff s
                    JOIN Roles r ON s.RoleId = r.RoleId
                    ORDER BY r.RoleName, s.LastName, s.FirstName";

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        var staffByRole = new Dictionary<string, List<string>>();

                        while (reader.Read())
                        {
                            string roleName = reader.GetString(0);
                            string fullName = reader.GetString(1) + " " + reader.GetString(2);

                            if (!staffByRole.ContainsKey(roleName))
                            {
                                staffByRole[roleName] = new List<string>();
                            }
                            staffByRole[roleName].Add(fullName);
                        }

                        foreach (var group in staffByRole)
                        {
                            Console.WriteLine($"{group.Value.Count} staff as {group.Key}:");
                            foreach (var name in group.Value)
                            {
                                Console.WriteLine(name);
                            }
                        }
                    }
                }
                else if (int.TryParse(roleChoice, out int roleChoiceInt))
                {
                    // Query to check if the roleId exists
                    string checkRoleQuery = "SELECT COUNT(*) FROM Roles WHERE RoleId = @RoleId";
                    using (var command = new SqlCommand(checkRoleQuery, connection))
                    {
                        command.Parameters.AddWithValue("@RoleId", roleChoiceInt);
                        int roleCount = (int)command.ExecuteScalar();

                        if (roleCount > 0)
                        {
                            // Query to get staff for the selected role
                            string staffQuery = @"
                            SELECT s.FirstName, s.LastName, r.RoleName
                            FROM Staff s
                            JOIN Roles r ON s.RoleId = r.RoleId
                            WHERE s.RoleId = @RoleId";

                            using (var staffCommand = new SqlCommand(staffQuery, connection))
                            {
                                staffCommand.Parameters.AddWithValue("@RoleId", roleChoiceInt);
                                using (var reader = staffCommand.ExecuteReader())
                                {
                                    var staff = new List<string>();
                                    string staffRoleName = null;

                                    while (reader.Read())
                                    {
                                        if (staffRoleName == null)
                                        {
                                            staffRoleName = reader.GetString(2); // Get RoleName from first record
                                        }

                                        staff.Add(reader.GetString(0) + " " + reader.GetString(1));
                                    }

                                    Console.WriteLine($"{staff.Count} staff as {staffRoleName}");
                                    foreach (var name in staff)
                                    {
                                        Console.WriteLine(name);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }
            }
        }

        public static void DisplayStudents()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine("Do you want to sort the students by first or last name?");
                Console.WriteLine("1. First name");
                Console.WriteLine("2. Last name");
                string? sortChoice = Console.ReadLine();
                Console.WriteLine("");

                string orderBy = "";
                string order = "";

                switch (sortChoice)
                {
                    case "1":
                        orderBy = "FirstName";
                        break;
                    case "2":
                        orderBy = "LastName";
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        return;
                }

                Console.WriteLine("Do you want to sort the students in ascending or descending order?");
                Console.WriteLine("1. Ascending");
                Console.WriteLine("2. Descending");
                string? orderChoice = Console.ReadLine();
                Console.WriteLine("");

                switch (orderChoice)
                {
                    case "1":
                        order = "ASC";
                        break;
                    case "2":
                        order = "DESC";
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        return;
                }

                string query = $@"
                SELECT FirstName, LastName
                FROM Students
                ORDER BY {orderBy} {order}";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader.GetString(0);
                        string lastName = reader.GetString(1);
                        Console.WriteLine($"{firstName} {lastName}");
                    }
                }
            }
        }

        public static void DisplayAllCourses()
        {
            using (var context = new SchoolContext())
            {
                // Prints a list of all courses
                var courses = context.Courses.ToList();
                foreach (var c in courses)
                {
                    Console.WriteLine(c.CourseId + ". " + c.CourseName);
                }
            }
        }

        public static void DisplayStudentsInCourse()
        {
            /* Display all students in a certain class.
             * The user must first see a list of all the classes that exist,
             * then the user can choose one of the classes and then
             * all the students in that class will be printed.
             */
            DisplayAllCourses();
            using (var context = new SchoolContext())
            {
                Console.WriteLine("Which class do you want to see? Type a corresponding number.");
                string? classChoice = Console.ReadLine();
                Console.WriteLine("");

                // Tries to parse the choice and checks if any courses can be found with that ID
                var courses = context.Courses.ToList();
                if (int.TryParse(classChoice, out int classChoiceInt) && courses.Any(c => c.CourseId == classChoiceInt))
                {
                    // Selects all the students where their enrolment course ID equals to user choice
                    var studentsInCourse = context.Enrolments
                        .Where(e => e.CourseId == classChoiceInt)
                        .Select(e => e.Student)
                        .ToList();

                    string courseName = courses.First(c => c.CourseId == classChoiceInt).CourseName;
                    Console.WriteLine($"Students in {courseName}:");

                    // Prints all the students
                    foreach (var student in studentsInCourse)
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }
            }
        }

        public static void DisplayGradesFromLastMonth()
        {
            /* Display all grades set in the last month.
             * The user immediately gets a list of all the grades set in the last month,
             * where the student's name, the name of the course and the grade appear.
             */
            using (var context = new SchoolContext())
            {
                // Gets the current date and subtracts 30 days
                DateTime lastMonth = DateTime.Now.AddDays(-30);

                // Selects all the enrolments where the enrolment date is after the last month
                var grades = context.Enrolments
                    .Include(e => e.Student)   // Include related Student
                    .Include(e => e.Course)    // Include related Course
                    .Include(e => e.Staff)     // Include related Staff
                    .Where(e => e.GradeDate > lastMonth)
                    .ToList();

                if (grades.Count == 0 || grades == null)
                {
                    Console.WriteLine("No grades set in the last month.");
                }
                else
                {
                    // Prints all the grades
                    foreach (var grade in grades)
                    {
                        Console.WriteLine($"{grade.Student.FirstName} {grade.Student.LastName} - {grade.Course.CourseName}: {grade.Grade}. Graded on {grade.GradeDate:D}");
                    }
                }
            }
        }

        public static void DisplayGradesFromCourse()
        {
            /* Display a list of all courses and the average grade
             * that the students got in that course as well as the highest
             * and lowest grade that someone got in the course.
             * The user immediately gets a list of all courses in the database,
             * the average grade and the highest and lowest grade for each course.
             */
            DisplayAllCourses();
            using (var context = new SchoolContext())
            {
                Console.WriteLine("Which class do you want to see? Type a corresponding number.");
                string? classChoice = Console.ReadLine();
                Console.WriteLine("");

                // Tries to parse the choice and checks if any courses can be found with that ID
                var courses = context.Courses.ToList();
                if (int.TryParse(classChoice, out int classChoiceInt) && courses.Any(c => c.CourseId == classChoiceInt))
                {
                    // Makes a list of all grades from a specific course
                    var grades = context.Enrolments
                        .Where(e => e.CourseId == classChoiceInt)
                        .Select(e => e.Grade)
                        .ToList();

                    // Grade to numeric mapping
                    Dictionary<string, int> gradeToPoints = new Dictionary<string, int>
                    {
                        { "A", 5 },
                        { "B", 4 },
                        { "C", 3 },
                        { "D", 2 },
                        { "E", 1 },
                        { "F", 0 }
                    };

                    // Numeric to grade mapping
                    Dictionary<int, string> pointsToGrade = new Dictionary<int, string>
                    {
                        { 5, "A" },
                        { 4, "B" },
                        { 3, "C" },
                        { 2, "D" },
                        { 1, "E" },
                        { 0, "F" }
                    };

                    // Calculate the average points
                    double averagePoints = grades
                        .Where(grade => gradeToPoints.ContainsKey(grade)) // Ensure valid grades
                        .Select(grade => gradeToPoints[grade]) // Convert grades to points
                        .Average();

                    Console.WriteLine($"Average Points: {averagePoints:F2}");

                    // Convert back to a grade if needed (round to the nearest whole number)
                    int roundedPoints = (int)Math.Round(averagePoints);
                    string averageGrade = pointsToGrade.ContainsKey(roundedPoints) ? pointsToGrade[roundedPoints] : "?";

                    Console.WriteLine($"Average Grade: {averageGrade}");
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }
            }
        }

        public static void AddStudent()
        {
            /* Add a new student
             * The user gets the opportunity to enter information about
             * a new student and that data is then saved in the database.
             */
            using (var context = new SchoolContext())
            {
                // Check first name
                Console.WriteLine("Enter the student's first name:");
                string? firstName = Console.ReadLine();
                // Check if the first name is null or empty
                while (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine("First name cannot be empty. Please enter a valid first name:");
                    firstName = Console.ReadLine();
                }

                // Check last name
                Console.WriteLine("Enter the student's last name:");
                string? lastName = Console.ReadLine();
                // Check if the last name is null or empty
                while (string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("Last name cannot be empty. Please enter a valid last name:");
                    lastName = Console.ReadLine();
                }

                // Check personal number
                Console.WriteLine("Enter the student's personal number (YYYYMMDDNNNN):");
                long? personalNumber = long.Parse(Console.ReadLine());
                while (context.Students.Any(s => s.PersonalNumber == personalNumber))
                {
                    Console.WriteLine("A student with that personal number already exists. Please enter a different personal number (YYYYMMDDNNNN):");
                    personalNumber = long.Parse(Console.ReadLine());
                }

                // Creates a new student object
                var newStudent = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalNumber = (long)personalNumber
                };

                // Adds the new student to the database
                context.Students.Add(newStudent);
                context.SaveChanges();

                Console.WriteLine("Student added successfully.");
            }
        }

        public static void AddStaff()
        {
            /* Add new staff.
             * The user gets the opportunity to enter information about
             * a new employee and that data is then saved in the database.
             */
            using (var context = new SchoolContext())
            {
                // Check first name
                Console.WriteLine("Enter the staff's first name:");
                string? firstName = Console.ReadLine();
                // Check if the first name is null or empty
                while (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine("First name cannot be empty. Please enter a valid first name:");
                    firstName = Console.ReadLine();
                }

                // Check last name
                Console.WriteLine("Enter the staff's last name:");
                string? lastName = Console.ReadLine();
                // Check if the last name is null or empty
                while (string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("Last name cannot be empty. Please enter a valid last name:");
                    lastName = Console.ReadLine();
                }

                DisplayStaffRoles();

                Console.WriteLine("What role does this new staff have? Type a corresponding number.");
                string? roleChoice = Console.ReadLine();
                Console.WriteLine("");

                //Do a while loop as long as the role id doesn't exist
                while (int.TryParse(roleChoice, out var roleInt) == false && context.Roles.ToList().Any(r => r.RoleId == roleInt) == false)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid role number.");
                    //DisplayStaffRoles();

                    Console.WriteLine("What role does this new staff have? Type a corresponding number.");
                    roleChoice = Console.ReadLine();
                }

                int.TryParse(roleChoice, out var roleChoiceInt);

                // Create a Role object based on the user's choice
                var selectedRole = context.Roles.FirstOrDefault(r => r.RoleId == roleChoiceInt);

                // Creates a new staff object
                var newStaff = new Staff
                {
                    FirstName = firstName,
                    LastName = lastName,
                    RoleId = roleChoiceInt,
                    Role = selectedRole
                };

                // Adds the new staff to the database
                context.Staff.Add(newStaff);
                context.SaveChanges();

                Console.WriteLine("Staff added successfully.");
            }
        }
    }
}
