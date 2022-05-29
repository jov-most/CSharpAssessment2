using Microsoft.Data.Sqlite;
using HolmesglenStudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolmesglenStudentManagementSystem.DataAccessLayer
{
    public class SubjectDAL
    {        
        private SqliteConnection Connection;

        public SubjectDAL(SqliteConnection connection)
        {
            // connect to the target database
            Connection = connection;
        }
        // create
        public void Create(Subject subject)
        {
            // challenge yourself
            Connection.Open();

            var command = Connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Subject
                (SubjectID, FirstName, LastName, Email)
                VALUES(@a, @b, @c, @d)
            ";
            command.Parameters.AddWithValue("a", subject.SubjectId);
            command.Parameters.AddWithValue("b", subject.Title);
            command.Parameters.AddWithValue("c", subject.NumberOfSession);
            command.Parameters.AddWithValue("d", subject.HourPerSession);

            // execute the query
            command.ExecuteReader();

            Connection.Close();
        }

        public Subject Read(string id)
        {
            Subject subject = null;
            Connection.Open();

            // build the query
            var command = Connection.CreateCommand();
            command.CommandText = @"
                SELECT SubjectID, Title, NumberOfSession, HourPerSession
                FROM Subject
                WHERE SubjectID = @a
            ";

            command.Parameters.AddWithValue("a", id);

            //execute the query
            var reader = command.ExecuteReader();
            if(reader.Read())
            {
                var subjectID = reader.GetString(0);
                var title = reader.GetString(1);
                var numberOfSession = Int32.Parse(reader.GetString(2));
                var hourPerSession = Int32.Parse(reader.GetString(3));
                subject = new Subject(subjectID, title, numberOfSession, hourPerSession);
            }  //else student = null
            
            Connection.Close();
            
            return subject;
        }

        // read all
        public List<Subject> ReadAll()
        {
            var subjects = new List<Subject>();

            // open the connection
            Connection.Open();

            // build the query command
            var command = Connection.CreateCommand();
            
            // construct the SQL statement
            command.CommandText = @"SELECT SubjectID, Title, NumberOfSession, HourPerSession
                                    FROM Subject";

            // execute query
            var reader = command.ExecuteReader();

            // get the result
            while( reader.Read())
            {
                var subjectId = reader.GetString(0);
                var subjectTitle = reader.GetString(1);
                var numOfSession = reader.GetInt32(2);
                var hourPerSession = reader.GetInt32(3);
                subjects.Add(new Subject(subjectId, subjectTitle, numOfSession, hourPerSession));
            }

            // close the connection
            Connection.Close();

            return subjects;
        }

        public void Update(Subject subject)
        {
            // open the connection
            Connection.Open();

            // get the data to put into the database (update)
            var subjectID = subject.SubjectId;
            var title = subject.Title;
            var num_sessions = subject.NumberOfSession;
            var hours_per_session = subject.HourPerSession;

            // put the data into the database
            var command = Connection.CreateCommand();
            command.CommandText = @"
                UPDATE Subject
                SET SubjectID = @a,
                    Title = @b,
                    NumberOfSession = @c,
                    HourPerSession = @d
                WHERE SubjectID = @a
                ";
            command.Parameters.AddWithValue("a", subjectID);
            command.Parameters.AddWithValue("b", title);
            command.Parameters.AddWithValue("c", num_sessions);
            command.Parameters.AddWithValue("d", hours_per_session);

            // execute the update on the database
            command.ExecuteNonQuery();

            //close the connection
            Connection.Close();
        }

        // delete
        public void Delete(string id)
        {
            // connect to the database
            Connection.Open();

            // build the SQL query command
            var subjectID = id;

            var command = Connection.CreateCommand();
            command.CommandText = @"
                    DELETE FROM Subject
                    WHERE SubjectID = @a
            ";
            command.Parameters.AddWithValue("a", subjectID);

            // execute the command on the database
            command.ExecuteNonQuery();

            // close the connection
            Connection.Close();
        }
    }
}
