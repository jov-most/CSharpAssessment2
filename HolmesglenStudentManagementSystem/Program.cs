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

            bool TESTING_STUDENT_DAL = false;
            bool TESTING_SUBJECT_DAL = true;
            bool TESTING_ENROLLMENT_DAL = false;

            /**** STUDENT DAL *****/
            if (TESTING_STUDENT_DAL)
            {
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
                foreach (var element in students_list)
                {
                    Console.WriteLine("The student is {0} {1} and their email address is {2}", element.FirstName, element.LastName, element.Email);
                }

                Console.WriteLine("");
                Console.WriteLine("");

                // YOU NEED TO CHANGE THE ID OF THE NEW RECORD TO CREATE WITH EVERY TEST RUN, OTHERWISE
                // YOU WILL GET A RUNTIME ERROR THAT THE "UNIQUE ID" CONSTRAINT OF SQL HAS NOT BEEN MET.
                /* TEST STUDENT DAL - CREATE NEW RECORD */
                // Build the student object with data
                Student student_create = new Student("21", "James", "Murdoch", "james.murdoch@gmail.com");
                // Create the new student in the database
                studentDAL2.Create(student_create);
                // Test whether the new student was put into the database by reading from the database
                student = studentDAL.Read("6");

                Console.WriteLine("The new student in the database is: " + student.FirstName + " " + student.LastName);

                Console.WriteLine("");
                Console.WriteLine("");


                /* TEST STUDENT DAL - DELETE RECORD */
                //studentDAL.Delete("St0010");
                List<Student> students_list_delete_test = studentDAL2.ReadAll();
                Console.WriteLine("********* DELETE RECORD EXECUTED ***********");
                foreach (var element in students_list_delete_test)
                {
                    Console.WriteLine("The student is {0} {1} and their email address is {2}", element.FirstName, element.LastName, element.Email);
                }

                Console.WriteLine("");
                Console.WriteLine("");

                /* TEST STUDENT DAL - UPDATE RECORD */
                //build the data to update into the database
                Student new_student = new Student("13", "Jimmy", "Barnes", "jimmy.b@hotmail.com");

                // update the database record
                studentDAL2.Update(new_student);

                Console.WriteLine("********* UPDATE RECORD EXECUTED ***********");
                students_list = studentDAL2.ReadAll();
                foreach (var element in students_list_delete_test)
                {
                    Console.WriteLine("The student is {0} {1} and their email address is {2}", element.FirstName, element.LastName, element.Email);
                }

                Console.WriteLine("");
                Console.WriteLine("");
            }


            /**** ENROLLMENT DAL *****/
            if (TESTING_ENROLLMENT_DAL)
            {
                /* TESTING ENROLMENT DAL - READ */
                EnrollmentDAL enrollmentDAL = new EnrollmentDAL(sqlite_conn);

                Enrollment enrollment = new Enrollment();
                enrollment = enrollmentDAL.Read(1);

                Console.WriteLine("Enrollment ID is: " + enrollment.enrollmentID);
                Console.WriteLine("Student ID is: " + enrollment.studentID_FK);
                Console.WriteLine("Subject ID is: " + enrollment.subjectID_FK);

                /* TESTING ENROLMENT DAL - REAL ALL */
                List<Enrollment> enrollment_list = enrollmentDAL.ReadAll();
                foreach (var element in enrollment_list)
                {
                    Console.WriteLine("The enrollment ID is {0}, the student ID is {1} and the subject ID is {2}", element.enrollmentID, element.studentID_FK, element.subjectID_FK);
                }

                Console.WriteLine("");
                Console.WriteLine("");

                /* TESTING ENROLLMENT DAL - DELETE */
                enrollmentDAL.Delete("7");
                Console.WriteLine("***** DELETE RECORD EXECUTED ******");
                enrollment_list = enrollmentDAL.ReadAll();
                foreach (var element in enrollment_list)
                {
                    Console.WriteLine("The enrollment ID is {0}, the student ID is {1} and the subject ID is {2}", element.enrollmentID, element.studentID_FK, element.subjectID_FK);
                }

                /* TESTING ENROLLMENT DAL - UPDATE */
                // Build the data to update the table with
                Enrollment enrollment_data = new Enrollment("4", "St0003", "New subject ID");

                enrollmentDAL.Update(enrollment_data);
                Console.WriteLine("***** UPDATE RECORD EXECUTED ******");
                enrollment_list = enrollmentDAL.ReadAll();
                foreach (var element in enrollment_list)
                {
                    Console.WriteLine("The enrollment ID is {0}, the student ID is {1} and the subject ID is {2}", element.enrollmentID, element.studentID_FK, element.subjectID_FK);
                }

                /* TESTING ENROLLMENT DAL - CREATE */
                // Build the data to put into the new database entry
                enrollment_data = new Enrollment("6", "St0003", "Su0002");

                // Push the data into a new database record
                enrollmentDAL.Create(enrollment_data);

                // Read from the database to verify that the new record was created
                Console.WriteLine("***** CREATE RECORD EXECUTED ******");
                enrollment_list = enrollmentDAL.ReadAll();
                foreach (var element in enrollment_list)
                {
                    Console.WriteLine("The enrollment ID is {0}, the student ID is {1} and the subject ID is {2}", element.enrollmentID, element.studentID_FK, element.subjectID_FK);
                }

            }

            /**** SUBJECT DAL ****/
            if (TESTING_SUBJECT_DAL)
            {
                /* TESTING SUBJECT DAL - CREATE */
                SubjectDAL subjectDAL = new SubjectDAL(sqlite_conn);

                // Build the data to create in the database
                Subject subject = new Subject("Su0005", "Intelligent Systems Engineering", 10, 2);

                // Print the database records to the console to know what's currently in there
                List<Subject> subject_list = subjectDAL.ReadAll();

                Console.WriteLine("***** ENROLMENT RECORDS ******");
                foreach (var element in subject_list)
                {
                    Console.WriteLine("The subject ID is {0}, the subject title is {1}, the number of sessions" +
                        " is {2} and the hour per session is {3}", element.SubjectId, element.Title, element.NumberOfSession, element.HourPerSession);
                }

                // Push the subject object into the database to create a new record
                subjectDAL.Create(subject);

                // Print the database records to console to verify the newly created record
                Console.WriteLine("***** CREATE RECORD EXECUTED ******");
                subject_list = subjectDAL.ReadAll();

                foreach (var element in subject_list)
                {
                    Console.WriteLine("The subject ID is {0}, the subject title is {1}, the number of sessions" +
                        " is {2} and the hour per session is {3}", element.SubjectId, element.Title, element.NumberOfSession, element.HourPerSession);
                }

            }


        }

    }
}
