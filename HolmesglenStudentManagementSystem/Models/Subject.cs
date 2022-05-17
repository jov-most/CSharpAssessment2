using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolmesglenStudentManagementSystem.Models
{
    public class Subject
    {
        public string SubjectId;
        public string Title;
        public int NumberOfSession;
        public int HourPerSession;

        public Subject()
        {
            SubjectId = "";
            Title = "";
            NumberOfSession = 0;
            HourPerSession = 0;
        }

        public Subject(string subjectId, string title, int numberOfSession, int hourPerSession)
        {
            SubjectId = subjectId;
            Title = title;
            NumberOfSession = numberOfSession;
            HourPerSession = hourPerSession;
        }
    }
}
