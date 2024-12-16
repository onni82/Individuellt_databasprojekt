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
	RoleName NVARCHAR(50)
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

-- Insert roles
INSERT INTO Roles (RoleName)
VALUES 
('Principal'),
('Vice Principal'),
('Math Teacher'),
('English Teacher'),
('History Teacher'),
('Science Teacher'),
('Physical Education Teacher'),
('Art Teacher'),
('Music Teacher'),
('Computer Science Teacher'),
('Librarian'),
('Counselor'),
('Nurse'),
('Janitor'),
('Secretary'),
('Administrator'),
('Substitute Teacher'),
('Language Teacher'),
('Business Teacher'),
('Social Studies Teacher');

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

-- Insert students
INSERT INTO Students (FirstName, LastName, PersonalNumber)
VALUES
('Aaron', 'Adams', 100001),
('Betty', 'Baker', 100002),
('Cathy', 'Carter', 100003),
('David', 'Davis', 100004),
('Ella', 'Evans', 100005),
('Frank', 'Ford', 100006),
('Grace', 'Green', 100007),
('Harry', 'Hall', 100008),
('Ivy', 'Irwin', 100009),
('Jack', 'Jones', 100010),
('Kara', 'King', 100011),
('Liam', 'Lewis', 100012),
('Mia', 'Martin', 100013),
('Noah', 'Nelson', 100014),
('Olivia', 'Owens', 100015),
('Peter', 'Parker', 100016),
('Quinn', 'Quincy', 100017),
('Rachel', 'Reed', 100018),
('Sam', 'Smith', 100019),
('Tina', 'Turner', 100020);

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
