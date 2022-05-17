using HolmesglenStudentManagementSystem.DataAccessLayer;
using HolmesglenStudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolmesglenStudentManagementSystem.BusinessLogicLayer
{
    public class StudentBLL
    {

        public List<Student> GetAll()
        {
            return AppDAL.Instance().StudentDALInstance.ReadAll();
        }

        public Student GetOne(string id)
        {
            return AppDAL.Instance().StudentDALInstance.Read(id);
        }

        public bool Create(Student student)
        {
            if(GetOne(student.Id) != null)
            {
                // if student id exists, return false
                return false;
            }
            else
            {
                // if student id does not exist, create it
                AppDAL.Instance().StudentDALInstance.Create(student);
            }

            return true;
        }

        public bool Update(Student student)
        {
            if (GetOne(student.Id) == null)
            {
                // if student id does not exist, return false
                return false;
            }
            else
            {
                // if student id exists, update it
                AppDAL.Instance().StudentDALInstance.Update(student);
            }

            return true;
        }

        public bool Delete(string id)
        {
            if (GetOne(id) == null)
            {
                // if student id does not exist, return false
                return false;
            }
            else
            {
                // if student id exists, delete it
                AppDAL.Instance().StudentDALInstance.Delete(id);
            }

            return true;
        }
    }
}
