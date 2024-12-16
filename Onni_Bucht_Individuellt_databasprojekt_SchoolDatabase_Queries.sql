-- Create the database in SQL Server Management Studio (SSMS). The database you create must follow your ER model.

-- Creates the database
CREATE DATABASE School;
GO

-- Using the database
USE School;
GO

-- Creates the roles table
CREATE TABLE Roles(
	RoleId INT PRIMARY KEY IDENTITY(1,1),
	RoleName NVARCHAR(50) NOT NULL,
	RoleMonthlyPay INT NOT NULL
);
GO

-- Creates the staff table
-- Has an FK that refers to the roles table
CREATE TABLE Staff(
	StaffId INT PRIMARY KEY IDENTITY(1,1),
	FirstName NVARCHAR(50),
	LastName NVARCHAR(50),
	RoleId INT NOT NULL,
	FOREIGN KEY(RoleId) REFERENCES Roles(RoleId)
);
GO

-- Creates the students table
CREATE TABLE Students(
	StudentId INT PRIMARY KEY IDENTITY(1,1),
	FirstName NVARCHAR(50),
	LastName NVARCHAR(50),
	PersonalNumber BIGINT NOT NULL
);
GO

-- Creates the courses table
CREATE TABLE Courses(
	CourseId INT PRIMARY KEY IDENTITY(1,1),
	CourseName NVARCHAR(50) NOT NULL
);
GO

-- Creates the enroment table
-- It has FKs pointing to the Students table, Courses table and to the staff who set the grade
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

-- Insert roles with corresponding monthly pay
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

-- Insert staff
INSERT INTO Staff (FirstName, LastName, RoleId)
VALUES
('Alice', 'Smith', 1),
('Bob', 'Johnson', 2),
('Charlie', 'Williams', 3),
('Diana', 'Brown', 4),
('Evan', 'Jones', 5),
('Fiona', 'Garcia', 6),
('George', 'Martinez', 7),
('Hannah', 'Rodriguez', 8),
('Ian', 'Hernandez', 9),
('Julia', 'Lopez', 10),
('Kevin', 'Gonzalez', 11),
('Laura', 'Wilson', 12),
('Mike', 'Anderson', 13),
('Nina', 'Thomas', 14),
('Oscar', 'Moore', 15),
('Paula', 'Taylor', 16),
('Quinn', 'Jackson', 17),
('Riley', 'White', 18),
('Sophia', 'Harris', 19),
('Tom', 'Clark', 20);
GO

-- Insert students
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

-- Insert courses
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

-- Insert enrolments with grades
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

-- Hur mycket betalar respektive avdelning ut i lön varje månad? (SQL i SSMS)
-- What's the sum of wages from each department/role?
SELECT R.RoleName AS Department, SUM(R.RoleMonthlyPay) AS MonthlyPayout
FROM Staff S
JOIN Roles R
ON S.RoleId = R.RoleId
GROUP BY R.RoleName
ORDER BY MonthlyPayout DESC;
GO

-- Hur mycket är medellönen för de olika avdelningarna? (SQL i SSMS)
-- What's the average pay of each department/role?
SELECT R.RoleName AS Department, AVG(R.RoleMonthlyPay) AS AverageSalary
FROM Staff S
JOIN Roles R
ON S.RoleId = R.RoleId
GROUP BY R.RoleName
ORDER BY AverageSalary DESC;
GO

-- Skapa en Stored Procedure som tar emot ett Id och returnerar viktig information om den elev som är registrerad med aktuellt id (SQL i SSMS)
-- Create a Stored Procedure that receives an ID and returns important information about the student who is registered with the current ID
CREATE PROCEDURE GetStudentInfo
    @StudentId INT
AS
BEGIN
	-- Hämtar grundläggande information om studenten
	-- Retrieves basic information about the student
	SELECT S.StudentId, S.FirstName + ' ' + S.LastName AS FullName, S.PersonalNumber
	FROM Students S
	WHERE S.StudentId = @StudentId;

	-- Hämtar kursinformation och betyg för studenten
	-- Retrieves course information and grades for the student
	SELECT C.CourseName, E.Grade, E.GradeDate
	FROM Enrolment E
	JOIN Courses C ON E.CourseId = C.CourseId
	WHERE E.StudentId = @StudentId;
END;
GO

-- Sätt betyg på en elev genom att använda Transactions ifall något går fel (SQL i SSMS)
-- Grade a student using Transactions in case something goes wrong
