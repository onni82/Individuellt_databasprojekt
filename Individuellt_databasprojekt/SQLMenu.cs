using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Individuellt_databasprojekt.Data;
using Individuellt_databasprojekt.Models;

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
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				string query = "SELECT CourseId, CourseName FROM Course";

				using (var command = new SqlCommand(query, connection))
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						int courseId = reader.GetInt32(0);
						string courseName = reader.GetString(1);
						Console.WriteLine($"{courseId}. {courseName}");
					}
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
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				Console.WriteLine("Which class do you want to see? Type a corresponding number.");
				string? classChoice = Console.ReadLine();
				Console.WriteLine("");

				string query = "SELECT CourseId, CourseName FROM Courses WHERE CourseId = @CourseId";

				if (int.TryParse(classChoice, out int classChoiceInt))
				{
					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@CourseId", classChoiceInt);
						using (var reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								string courseName = reader.GetString(1);

								string studentsQuery = @"
								SELECT s.FirstName, s.LastName
								FROM Students s
								JOIN Enrolments e ON s.StudentId = e.StudentId
								WHERE e.CourseId = @CourseId";

								using (var studentCommand = new SqlCommand(studentsQuery, connection))
								{
									studentCommand.Parameters.AddWithValue("@CourseId", classChoiceInt);
									using (var studentReader = studentCommand.ExecuteReader())
									{
										Console.WriteLine($"Students in {courseName}");
										while (studentReader.Read())
										{
											string firstName = studentReader.GetString(0);
											string lastName = studentReader.GetString(1);
											Console.WriteLine($"{firstName} {lastName}");
										}
									}
								}
							}
							else
							{
								Console.WriteLine("Invalid course choice.");
							}
						}
					}
				}
				else
				{
					Console.WriteLine("Invalid input.");
				}
			}
		}

		public static void DisplayGradesFromLastMonth()
		{
			/* Display all grades set in the last month.
			 * The user immediately gets a list of all the grades set in the last month,
			 * where the student's name, the name of the course and the grade appear.
			 */
			using (var connection = new SqlConnection(connectionString))
			{
				// Gets the current date and subtracts 30 days
				DateTime lastMonth = DateTime.Now.AddDays(-30);
				connection.Open();

				string query = @"
				SELECT s.FirstName, s.LastName, c.CourseName, e.Grade, e.GradeDate
				FROM Enrolments e
				JOIN Students s ON e.StudentId = s.StudentId
				JOIN Courses c ON e.CourseId = c.CourseId
				WHERE e.GradeDate > @LastMonth";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@LastMonth", lastMonth);
					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows)
						{
							Console.WriteLine("No grades set in the lst month.");
						}
						else
						{
							while (reader.Read())
							{
								string firstName = reader.GetString(0);
								string lastName = reader.GetString(1);
								string courseName = reader.GetString(2);
								string grade = reader.GetString(3);
								DateTime gradeDate = reader.GetDateTime(4);
								Console.WriteLine($"{firstName} {lastName} - {courseName}: {grade}. Graded on {gradeDate:D}");
							}
						}
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
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				Console.WriteLine("Which class do you want to see? Type a corresponding number.");
				string? classChoice = Console.ReadLine();
				Console.WriteLine("");

				if (int.TryParse(classChoice, out int classChoiceInt))
				{
					string gradesQuery = @"
					SELECT Grade
					FROM Enrolments
					WHERE CourseId = @CourseId";

					using (var command = new SqlCommand(gradesQuery, connection))
					{
						command.Parameters.AddWithValue("@CourseId", classChoiceInt);
						using (var reader = command.ExecuteReader())
						{
							var grades = new List<string>();
							while (reader.Read())
							{
								grades.Add(reader.GetString(0));
							}

							if (grades.Count == 0)
							{
								Console.WriteLine("No grades available for this course.");
							}
							else
							{
								// Calculate the average grade
								Dictionary<string, int> gradeToPoints = new Dictionary<string, int>
								{
									{ "A", 5 },
									{ "B", 4 },
									{ "C", 3 },
									{ "D", 2 },
									{ "E", 1 },
									{ "F", 0 }
								};

								var numericGrades = grades.Select(grade => gradeToPoints[grade]).ToList();

								double averagePoints = numericGrades.Average();
								int highestGrade = numericGrades.Max();
								int lowestGrade = numericGrades.Min();

								Dictionary<int, string> pointsToGrade = new Dictionary<int, string>
								{
									{ 5, "A" },
									{ 4, "B" },
									{ 3, "C" },
									{ 2, "D" },
									{ 1, "E" },
									{ 0, "F" }
								};

								string averageGrade = pointsToGrade[(int)Math.Round(averagePoints)];

								Console.WriteLine($"Average Grade: {averageGrade}");
								Console.WriteLine($"Highest Grade: {pointsToGrade[highestGrade]}");
								Console.WriteLine($"Lowest Grade: {pointsToGrade[lowestGrade]}");
							}
						}
					}
				}
				else
				{
					Console.WriteLine("Invalid course choice.");
				}
			}
		}

		public static void AddStudent()
		{
			/* Add a new student
			 * The user gets the opportunity to enter information about
			 * a new student and that data is then saved in the database.
			 */

			// Check first name
			Console.WriteLine("Enter the student's first name:");
			string firstName = Console.ReadLine();
			while (string.IsNullOrWhiteSpace(firstName))
			{
				Console.WriteLine("First name cannot be empty. Please enter a valid first name:");
				firstName = Console.ReadLine();
			}

			// Check last name
			Console.WriteLine("Enter the student's last name:");
			string lastName = Console.ReadLine();
			while (string.IsNullOrWhiteSpace(lastName))
			{
				Console.WriteLine("Last name cannot be empty. Please enter a valid last name:");
				lastName = Console.ReadLine();
			}

			// Check personal number
			Console.WriteLine("Enter the student's personal number (YYYYMMDDNNNN):");
			long personalNumber = long.Parse(Console.ReadLine());

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				// Check if student already exists based on personal number
				string checkStudentQuery = "SELECT COUNT(*) FROM Students WHERE PersonalNumber = @PersonalNumber";
				using (SqlCommand cmd = new SqlCommand(checkStudentQuery, connection))
				{
					cmd.Parameters.AddWithValue("@PersonalNumber", personalNumber);
					int existingStudentCount = (int)cmd.ExecuteScalar();
					if (existingStudentCount > 0)
					{
						Console.WriteLine("A student with that personal number already exists. Please enter a different personal number.");
						return;
					}
				}

				// Insert new student
				string insertStudentQuery = "INSERT INTO Students (FirstName, LastName, PersonalNumber) VALUES (@FirstName, @LastName, @PersonalNumber)";
				using (SqlCommand cmd = new SqlCommand(insertStudentQuery, connection))
				{
					cmd.Parameters.AddWithValue("@FirstName", firstName);
					cmd.Parameters.AddWithValue("@LastName", lastName);
					cmd.Parameters.AddWithValue("@PersonalNumber", personalNumber);
					cmd.ExecuteNonQuery();
				}

				Console.WriteLine("Student added successfully.");
			}
		}

		public static void AddStaff()
		{
			/* Add new staff.
			 * The user gets the opportunity to enter information about
			 * a new employee and that data is then saved in the database.
			 */

			//Check first name
			Console.WriteLine("Enter the staff's first name:");
			string firstName = Console.ReadLine();
			while (string.IsNullOrWhiteSpace(firstName))
			{
				Console.WriteLine("First name cannot be empty. Please enter a valid first name:");
				firstName = Console.ReadLine();
			}

			// Check last name
			Console.WriteLine("Enter the staff's last name:");
			string lastName = Console.ReadLine();
			while (string.IsNullOrWhiteSpace(lastName))
			{
				Console.WriteLine("Last name cannot be empty. Please enter a valid last name:");
				lastName = Console.ReadLine();
			}


		}

		// Helper method to check if role exists in the database
		public static bool RoleExists(int roleId, string connectionString)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				string roleCheckQuery = "SELECT COUNT(*) FROM Roles WHERE RoleId = @RoleId";
				using (SqlCommand cmd = new SqlCommand(roleCheckQuery, connection))
				{
					cmd.Parameters.AddWithValue("@RoleId", roleId);
					int roleCount = (int)cmd.ExecuteScalar();
					return roleCount > 0;
				}
			}
		}
	}
}
