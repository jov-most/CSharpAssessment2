using Microsoft.Data.Sqlite;
using HolmesglenStudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolmesglenStudentManagementSystem.DataAccessLayer
{
    public class EnrollmentDAL
    {
        private SqliteConnection Connection;

        public EnrollmentDAL(SqliteConnection connection)
        {
            //connect to the target database
            Connection = connection;
        }

        public object EnrollmentID { get; private set; }

        // create
        public void Create(Enrollment enrollment)
        {
            Connection.Open();

            var command = Connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Enrollment
                (EnrollmentID, StudentID_FK, SubjectID_FK)
                VALUES(@a, @b, @c)
            ";

            command.Parameters.AddWithValue("a", enrollment.enrollmentID);
            command.Parameters.AddWithValue("b", enrollment.studentID_FK);
            command.Parameters.AddWithValue("c", enrollment.subjectID_FK);

            // execute the query
            command.ExecuteReader();

            Connection.Close();
        }

        public Enrollment Read(int id)
        {
            Enrollment enrollment = null;
            Connection.Open();

            // build the query
            var command = Connection.CreateCommand();
            command.CommandText = @"
            SELECT EnrollmentID, StudentID_FK, SubjectID_FK
            FROM Enrollment
            WHERE EnrollmentID = @a
            ";

            command.Parameters.AddWithValue("a", id);

            //execute the query
            var reader = command.ExecuteReader();
            if(reader.Read())
            {
                var enrollmentID = reader.GetInt32(0);
                var studentID_FK = reader.GetString(1);
                var subjectID_FK = reader.GetString(2);

                enrollment = new Enrollment(enrollmentID.ToString(), studentID_FK, subjectID_FK);
            } // else enrollment = null

            Connection.Close();

            return enrollment;

        }

        // read all
        public List<Enrollment> ReadAll()
        {
            var enrollment = new List<Enrollment>();

            // open the connection
            Connection.Open();

            // build the query command
            var command = Connection.CreateCommand();

            //construct the SQL statement
            command.CommandText = @"SELECT EnrollmentID, StudentID_FK, SubjectID_FK
                                    FROM Enrollment";

            //execute query
            var reader = command.ExecuteReader();

            //get the result
            while( reader.Read())
            {
                var enrollmentID = reader.GetString(0);
                var studentID_FK = reader.GetString(1);
                var subjectID_FK = reader.GetString(2);
                enrollment.Add(new Enrollment(enrollmentID, studentID_FK, subjectID_FK));
            }

            // close the connection
            Connection.Close();

            return enrollment;
        }


        // update
        public void Update(Enrollment enrollment)
        {
            // open the connection
            Connection.Open();

            // get the data to put into the database (update)
            var enrollmentID = enrollment.enrollmentID;
            var studentID_FK = enrollment.studentID_FK;
            var subjectID_FK = enrollment.subjectID_FK;

            // put the data into the database
            var command = Connection.CreateCommand();
            command.CommandText = @"
                UPDATE Enrollment
                SET EnrollmentID = @a,
                    StudentID_FK = @b,
                    SubjectID_FK = @c
                WHERE EnrollmentID = @a
            ";
            command.Parameters.AddWithValue("a", enrollment.enrollmentID);
            command.Parameters.AddWithValue("b", enrollment.studentID_FK);
            command.Parameters.AddWithValue("c", enrollment.subjectID_FK);

            // execute the update on the database
            command.ExecuteNonQuery();

            // close the connection
            Connection.Close();
        }

        // delete
        public void Delete(string id)
        {
            // connect to the database
            Connection.Open();

            // build the SQL query command
            var enrollmentID = id;

            var command = Connection.CreateCommand();
            command.CommandText = @"
                    DELETE FROM Enrollment
                    WHERE EnrollmentID = @a
            ";
            command.Parameters.AddWithValue("a", enrollmentID);

            // execute the command on the database
            command.ExecuteNonQuery();

            // close the connection
            Connection.Close();
        }

    }
}
