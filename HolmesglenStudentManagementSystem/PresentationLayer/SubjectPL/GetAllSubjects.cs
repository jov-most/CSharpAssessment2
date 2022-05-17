using HolmesglenStudentManagementSystem.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolmesglenStudentManagementSystem.PresentationLayer.SubjectPL
{
    public class GetAllSubjects : PLBase
    {
        public override void Run()
        {
            var subjectBLL = new SubjectBLL();
            var subjects = subjectBLL.GetAll();

            foreach (var subject in subjects)
            {
                Console.WriteLine(subject.SubjectId + " - " + subject.Title + " - " + subject.NumberOfSession + " - " + subject.HourPerSession);
            }
        }
    }
}
