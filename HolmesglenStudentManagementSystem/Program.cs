using HolmesglenStudentManagementSystem.PresentationLayer;
using HolmesglenStudentManagementSystem.DataAccessLayer;
using System;
using HolmesglenStudentManagementSystem.PresentationLayer.StudentPL;
using HolmesglenStudentManagementSystem.PresentationLayer.SubjectPL;
using Microsoft.Data.Sqlite;
using HolmesglenStudentManagementSystem.Models;
using System.Collections.Generic;

namespace HolmesglenStudentManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sqlite_conn = new SqliteConnection(HolmesglenDB.ConnectionString);


            /* TESTING STUDENT DAL - READ ONE SQL RECORD */
            StudentDAL studentDAL = new StudentDAL(sqlite_conn);

            Student student = new Student();
            student = studentDAL.Read("St0001");

            Console.WriteLine("Student ID is: " + student.Id);
            Console.WriteLine("First name is: " + student.FirstName);
            Console.WriteLine("Last Name is: " + student.LastName);
            Console.WriteLine("Email is: " + student.Email);
            student = null;

            Console.WriteLine("");
            Console.WriteLine("");

            /* TESTING STUDENT DAL - READ ALL SQL RECORDS */
            StudentDAL studentDAL2 = new StudentDAL(sqlite_conn);
            List<Student> students_list = studentDAL2.ReadAll();
            // Console.WriteLine("The first student is " + students_list[0].FirstName + " " + students_list[0].LastName);
            // Console.WriteLine("The second student is " + students_list[1].FirstName);
            // Console.WriteLine("The third student is " + students_list[2].FirstName);
            // Console.WriteLine("The fourth student is " + students_list[3].FirstName);
            foreach (var element in students_list)
            {
                Console.WriteLine("The student is {0} {1} and their email address is {2}", element.FirstName, element.LastName, element.Email);
            }

            Console.WriteLine("");
            Console.WriteLine("");

            /* TEST STUDENT DAL - CREATE NEW RECORD */
            // Build the student object with data
            Student student_create = new Student("6", "Jovan", "Mostanovski", "jov.mostanovski@gmail.com");
            // Create the new student in the database
            studentDAL2.Create(student_create);
            // Test whether the new student was put into the database by reading from the database
            student = studentDAL.Read("6");
            
            Console.WriteLine("The new student in the database is: " + student.FirstName + " " + student.LastName);

            Console.WriteLine("");
            Console.WriteLine("");


            /* TEST STUDENT DAL - DELETE RECORD */
            studentDAL.Delete("6");
            List<Student> students_list_delete_test = studentDAL2.ReadAll();
            Console.WriteLine("********* DELETE RECORD EXECUTED ***********");
            foreach (var element in students_list_delete_test)
            {
                Console.WriteLine("The student is {0} {1} and their email address is {2}", element.FirstName, element.LastName, element.Email);
            }

            Console.WriteLine("");
            Console.WriteLine("");



            /* TESTING ENROLMENT DAL */
            EnrollmentDAL enrollmentDAL = new EnrollmentDAL(sqlite_conn);

            Enrollment enrollment = new Enrollment();
            enrollment = enrollmentDAL.Read(1);

            Console.WriteLine("Enrollment ID is: " + enrollment.enrollmentID);
            Console.WriteLine("Student ID is: " + enrollment.studentID_FK);
            Console.WriteLine("Subject ID is: " + enrollment.subjectID_FK);

        }

    }
}
