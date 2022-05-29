using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolmesglenStudentManagementSystem.Models
{
    public class Enrollment
    {
        public string enrollmentID;
        public string studentID_FK;
        public string subjectID_FK;

        public Enrollment()
        {
            enrollmentID = "";
            studentID_FK = "";
            subjectID_FK = "";
        }

        public Enrollment(string enroll_id, string student_id, string subject_id)
        {
            enrollmentID = enroll_id;
            studentID_FK = student_id;
            subjectID_FK = subject_id;
        }



    }
}

  
