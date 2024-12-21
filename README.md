# Individual database project
## About the assignment

This is the last practical assignment in the course. It is a fairly free assignment where you must prove that you can build more complex systems with databases. The assignment builds on labs 2 and 3.

## Basic criteria

### For `G` the following must be met:

- Call the database with both SQL and an ORM

- Construct well-functioning SQL code

- Create a clear, usable and stable database model

### For `VG` you must also:

- Write a reflection in which you reason in a nuanced/critical way about and justify the database model you have developed. You must also reason in a nuanced way about the performance and suitability of the SQL code you have produced.

You submit this reflection as a PDF or text file together with your submission, the file name can be: Reasoning.pdf

## Project

In this project, you will build a fully functional application for the fictional school you worked with in the latest labs. You will therefore create a console application that the school can use and that has the functionality requested below.

### Functions in the program:

Here are the functions you will build in your program.

- ➡️ There must be a menu where you can choose to display different data requested by the school. (Console)
- ➡️ The school wants to be able to produce an overview of all staff, showing their names and positions as well as how many years they have worked at the school. The administrator also wants to be able to save new staff. (SQL in SSMS)
- ➡️ We want to save students and see which class they are in. We want to be able to save grades for a student in each course they have taken and we want to be able to see which teacher set the grade. Grades should also have a date when they were set. (SQL in SSMS)
- ➡️ How many teachers work in the different departments? (EF in VS)
- ➡️ Show information about all students (EF in VS)
- ➡️ Show a list of all (active) courses (EF in VS)
- ➡️ How much does each department pay out in salary each month? (SQL in SSMS)
- ➡️ What is the average salary for the different departments? (SQL in SSMS)
- ➡️ Create a Stored Procedure that receives an Id and returns important information about the student who is registered with the current id. (SQL in SSMS)
- ➡️ Grade a student by using Transactions in case something goes wrong. (SQL in SSMS)

⚙ Extra challenges
1. Show information about a student, which class they belong to and which teacher(s) they have and what grades they have received in a specific course. (SQL)
2. Create a View that shows all teachers and which courses they are responsible for. (SQL in SSMS)
3. Update/correct a student's information via code. (EF in VS)

# Your submission

- Submit the assignment in Canvas
- Submit a text file containing the link to a Github repo with your code + an SQL file with your database
- If you are going for VG also a PDF or text file with your reflection as above.