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

        }

        public Subject Read(string id)
        {
            Subject subject = null;
            
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
            
        }

        public void Delete(string id)
        {
            
        }
    }
}
