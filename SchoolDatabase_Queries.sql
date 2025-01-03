-- Create the database in SQL Server Management Studio (SSMS). The database you create must follow your ER model.

-- Creates the database.
CREATE DATABASE School;
GO

-- Using the database.
USE School;
GO

-- Creates the roles table.
CREATE TABLE Roles(
	RoleId INT PRIMARY KEY IDENTITY(1,1),
	RoleName NVARCHAR(50) NOT NULL,
	RoleMonthlyPay INT NOT NULL
);
GO

-- Creates the staff table.
-- Has an FK that refers to the roles table.
CREATE TABLE Staff(
	StaffId INT PRIMARY KEY IDENTITY(1,1),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	HireDate DATE,
	RoleId INT NOT NULL,
	FOREIGN KEY(RoleId) REFERENCES Roles(RoleId)
);
GO

-- Creates the students table.
CREATE TABLE Students(
	StudentId INT PRIMARY KEY IDENTITY(1,1),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	PersonalNumber BIGINT NOT NULL
);
GO

-- Creates the courses table.
CREATE TABLE Courses(
	CourseId INT PRIMARY KEY IDENTITY(1,1),
	CourseName NVARCHAR(50) NOT NULL
);
GO

-- Creates the enroment table.
-- It has FKs pointing to the Students table, Courses table and to the staff who set the grade.
CREATE TABLE Enrolment(
	EnrolmentId INT PRIMARY KEY IDENTITY(1,1),
	CourseId INT NOT NULL,
	StudentId INT NOT NULL,
	StaffId INT NOT NULL,
	Grade CHAR(1),
	GradeDate DATETIME,
	FOREIGN KEY(CourseId) REFERENCES Courses(CourseId),
	FOREIGN KEY(StudentId) REFERENCES Students(StudentId),
	FOREIGN KEY(StaffId) REFERENCES Staff(StaffId)
);
GO

-- Insert roles with corresponding monthly pay.
INSERT INTO Roles (RoleName, RoleMonthlyPay)
VALUES 
('Principal', 55000),
('Vice Principal', 50000),
('Math Teacher', 45000),
('English Teacher', 45000),
('History Teacher', 45000),
('Science Teacher', 45000),
('Physical Education Teacher', 40000),
('Art Teacher', 40000),
('Music Teacher', 40000),
('Computer Science Teacher', 45000),
('Librarian', 35000),
('Counselor', 35000),
('Nurse', 35000),
('Janitor', 30000),
('Secretary', 30000),
('Administrator', 40000),
('Substitute Teacher', 35000),
('Language Teacher', 45000),
('Business Teacher', 45000),
('Social Studies Teacher', 45000);
GO

-- Insert staff.
INSERT INTO Staff (FirstName, LastName, HireDate, RoleId)
VALUES
('Alice', 'Smith', '2000-08-15', 1),
('Bob', 'Johnson', '2002-03-10', 2),
('Charlie', 'Williams', '2005-09-01', 3),
('Diana', 'Brown', '2007-04-15', 4),
('Evan', 'Jones', '2009-06-20', 5),
('Fiona', 'Garcia', '2011-11-05', 6),
('George', 'Martinez', '2013-08-10', 7),
('Hannah', 'Rodriguez', '2015-01-25', 8),
('Ian', 'Hernandez', '2016-09-15', 9),
('Julia', 'Lopez', '2017-12-10', 10),
('Kevin', 'Gonzalez', '2018-05-20', 11),
('Laura', 'Wilson', '2019-07-15', 12),
('Mike', 'Anderson', '2020-02-10', 13),
('Nina', 'Thomas', '2021-03-25', 14),
('Oscar', 'Moore', '2022-10-10', 15),
('Paula', 'Taylor', '2023-01-05', 16),
('Quinn', 'Jackson', '2023-05-15', 17),
('Riley', 'White', '2023-08-20', 18),
('Sophia', 'Harris', '2023-11-05', 19),
('Tom', 'Clark', '2023-12-01', 20);
GO

-- Insert students.
INSERT INTO Students (FirstName, LastName, PersonalNumber)
VALUES
('Aaron', 'Adams', 200403159566),
('Betty', 'Baker', 200402183464),
('Cathy', 'Carter', 200410179779),
('David', 'Davis', 200408272921),
('Ella', 'Evans', 200406226725),
('Frank', 'Ford', 200403025461),
('Grace', 'Green', 200404195266),
('Harry', 'Hall', 200408034476),
('Ivy', 'Irwin', 200409263843),
('Jack', 'Jones', 200406173588),
('Kara', 'King', 200405285521),
('Liam', 'Lewis', 200405163667),
('Mia', 'Martin', 200410154509),
('Noah', 'Nelson', 200404069740),
('Olivia', 'Owens', 200402261238),
('Peter', 'Parker', 200405197824),
('Quinn', 'Quincy', 200408250249),
('Rachel', 'Reed', 200403166288),
('Sam', 'Smith', 200403248523),
('Tina', 'Turner', 200404181512);
GO

-- Insert courses.
INSERT INTO Courses (CourseName)
VALUES
('Math 101'),
('English 101'),
('History 101'),
('Science 101'),
('Art 101'),
('Music 101'),
('Physical Education 101'),
('Computer Science 101'),
('Business 101'),
('Social Studies 101');
GO

-- Insert enrolments with grades.
DECLARE @Today DATETIME = GETDATE();
DECLARE @LastMonth DATETIME = DATEADD(MONTH, -1, @Today);

INSERT INTO Enrolment (CourseId, StudentId, StaffId, Grade, GradeDate)
VALUES
(1, 1, 3, 'A', DATEADD(DAY, -7, @Today)),
(2, 2, 4, 'B', DATEADD(DAY, -14, @Today)),
(3, 3, 5, 'C', DATEADD(DAY, -21, @Today)),
(4, 4, 6, 'A', DATEADD(DAY, -3, @Today)),
(5, 5, 7, 'B', DATEADD(DAY, -6, @Today)),
(6, 6, 8, 'C', DATEADD(DAY, -18, @Today)),
(7, 7, 9, 'A', DATEADD(DAY, -9, @Today)),
(8, 8, 10, 'B', DATEADD(DAY, -20, @Today)),
(9, 9, 11, 'C', DATEADD(DAY, -11, @Today)),
(10, 10, 12, 'A', DATEADD(DAY, -1, @Today)),
(1, 11, 3, 'B', DATEADD(DAY, -25, @Today)),
(2, 12, 4, 'C', DATEADD(DAY, -10, @Today)),
(3, 13, 5, 'A', DATEADD(DAY, -4, @Today)),
(4, 14, 6, 'B', DATEADD(DAY, -2, @Today)),
(5, 15, 7, 'C', DATEADD(DAY, -5, @Today)),
(6, 16, 8, 'A', DATEADD(DAY, -13, @Today)),
(7, 17, 9, 'B', DATEADD(DAY, -22, @Today)),
(8, 18, 10, 'C', DATEADD(DAY, -12, @Today)),
(9, 19, 11, 'A', DATEADD(DAY, -15, @Today)),
(10, 20, 12, 'B', DATEADD(DAY, -19, @Today));
GO

-- The school wants to be able to produce an overview of all staff,
-- which shows their names and the positions they hold,
-- as well as how many years they have worked at the school.
-- The administrator also wants to be able to save new staff.
SELECT
	CONCAT(FirstName, ' ', LastName) AS FullName,
	Roles.RoleName AS Position,
	DATEDIFF(YEAR, HireDate, GETDATE()) AS YearsWorked
FROM Staff
JOIN Roles ON Staff.RoleId = Roles.RoleId
ORDER BY YearsWorked DESC;
GO

-- Create a stored procedure to add new staff.
-- Example code:
-- EXEC AddStaff 
--     @FirstName = 'John', 
--     @LastName = 'Doe', 
--     @HireDate = '2024-01-15', 
--     @RoleId = 3;
-- GO
CREATE PROCEDURE AddStaff
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@HireDate DATE,
	@RoleId INT
AS
BEGIN
	INSERT INTO Staff (FirstName, LastName, HireDate, RoleId)
	VALUES (@FirstName, @LastName, @HireDate, @RoleId)

	PRINT 'New staff member added successfully.';
END;
GO

-- We want to save students and see which class they are in.
-- We want to be able to save grades for a student in each course they read
-- and we want to be able to see which teacher gave the grade.
-- Grades must also have a date on which they were set.
-- Example code:
-- EXEC AddStudent
--     @FirstName = 'John', 
--     @LastName = 'Doe', 
--     @PersonalNumber = 200404205623,
--     @CourseId = 1; -- This will assign to Math 101. You can replace 1 with NULL if no enrolment is necessary.
-- GO
CREATE PROCEDURE AddStudent
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@PersonalNumber BIGINT,
	@CourseId INT = NULL -- Optional: Assign to a course
AS
BEGIN
	INSERT INTO Students (FirstName, LastName, PersonalNumber)
	VALUES (@FirstName, @LastName, @PersonalNumber)

	-- If a CourseId is provided, enroll the student in the course
	IF @CourseId IS NOT NULL
	BEGIN
		INSERT INTO Enrolment (CourseId, StudentId, Grade, GradeDate, StaffId)
		VALUES (@CourseId, SCOPE_IDENTITY(), NULL, NULL, NULL); -- Null grade & teacher initially
	END;

	PRINT 'New student added successfully.';
END;
GO

-- Procedure to create enrolment for a student
-- Example code:
-- EXEC CreateEnrolment
--     @StudentId = 1,         -- Aaron Adams
--     @CourseId = 1,          -- Math 101
--     @StaffId = 3,           -- Math Teacher: Charlie Williams
--     @Grade = 'A',           -- 'A' can be replaced with NULL if no grade is set
--     @GradeDate = GETDATE(); -- GETDATE() can be replaced with NULL if no grade date is available
-- GO
CREATE PROCEDURE CreateEnrolment
	@StudentId INT,
	@CourseId INT,
	@StaffId INT,
	@Grade CHAR(1),
	@GradeDate DATETIME
AS
BEGIN
	-- Start the transaction
	BEGIN TRANSACTION;

	BEGIN TRY
		-- Insert into the Enrolment table
		INSERT INTO Enrolment (CourseId, StudentId, Grade, GradeDate, StaffId)
		VALUES (@CourseId, SCOPE_IDENTITY(), NULL, NULL, NULL); -- Null grade & teacher initially

		-- Commit transaction if successful
		COMMIT TRANSACTION;
		PRINT 'Enrolment saved successfully';
	END TRY
	BEGIN CATCH
		-- Roll back the transaction in case of an error
		ROLLBACK TRANSACTION;

		-- Rethrow error for debugging
		THROW;
	END CATCH
END;
GO

-- Procedure to update grade for a student in a course
-- Example code:
-- EXEC UpdateGrade
--     @StudentId = 1, -- Aaron Adams
--     @CourseId = 1,  -- Math 101
--     @StaffId = 3,   -- Math Teacher: Charlie Williams
--     @Grade = 'A',
--     @GradeDate = GETDATE();
-- GO
CREATE PROCEDURE UpdateGrade
	@StudentId INT,
	@CourseId INT,
	@StaffId INT,
	@Grade CHAR(1),
	@GradeDate DATETIME
AS
BEGIN
	-- Start the transaction
	BEGIN TRANSACTION;

	BEGIN TRY
		-- Update the grade in the Enrolment table
		UPDATE Enrolment
		SET Grade = @Grade,
			GradeDate = @GradeDate,
			StaffId = @StaffId
		WHERE StudentId = @StudentId AND CourseId = @CourseId;

		-- Commit transaction if successful
		COMMIT TRANSACTION;
		PRINT 'Grade saved successfully';
	END TRY
	BEGIN CATCH
		-- Roll back the transaction in case of an error
		ROLLBACK TRANSACTION;

		-- Rethrow error for debugging
		THROW;
	END CATCH
END;
GO

-- Create a view to display grades with teacher details
-- Example run code:
-- SELECT *
-- FROM GradesWithTeacherDetails
-- ORDER BY GradeDate DESC;
-- GO
CREATE VIEW GradesWithTeacherDetails AS
SELECT
	CONCAT(S.FirstName, ' ', S.LastName) AS StudentName,
	C.CourseName,
	CONCAT(T.FirstName, ' ', T.LastName) AS TeacherName,
	E.Grade,
	E.GradeDate
FROM Enrolment E
JOIN Students S ON E.StudentId = S.StudentId
JOIN Courses C ON E.CourseId = C.CourseId
LEFT JOIN Staff T ON E.StaffId = T.StaffId;
GO

-- What's the sum of wages from each department/role?
SELECT
	R.RoleName AS Department,
	SUM(R.RoleMonthlyPay) AS MonthlyPayout
FROM Staff S
JOIN Roles R ON S.RoleId = R.RoleId
GROUP BY R.RoleName
ORDER BY MonthlyPayout DESC;
GO

-- What's the average pay of each department/role?
SELECT
	R.RoleName AS Department,
	AVG(R.RoleMonthlyPay) AS AverageSalary
FROM Staff S
JOIN Roles R
ON S.RoleId = R.RoleId
GROUP BY R.RoleName
ORDER BY AverageSalary DESC;
GO

-- Create a Stored Procedure that receives an ID and returns important information about the student who is registered with the current ID.
-- Example: EXEC GetStudentInfo @StudentId = 1;
CREATE PROCEDURE GetStudentInfo
    @StudentId INT
AS
BEGIN
	-- Retrieves basic information about the student.
	SELECT
		S.StudentId,
		S.FirstName + ' ' + S.LastName AS FullName,
		S.PersonalNumber
	FROM Students S
	WHERE S.StudentId = @StudentId;

	-- Retrieves course information and grades for the student.
	SELECT
		C.CourseName,
		E.Grade,
		E.GradeDate
	FROM Enrolment E
	JOIN Courses C ON E.CourseId = C.CourseId
	WHERE E.StudentId = @StudentId;
END;
GO
