using _475_Lab_4_Part_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IBusinessLayer
    {
        IList<Standard> getAllStandards();
        Standard GetStandardByID(int id);
        void addStandard(Standard standard);
        void updateStudent(Standard standard);
        void RemoveStudent(Standard standard);

        IList<Student> getAllStudents();
        void addStudent(Student student);
        void UpdateStudent(Student student);
        void removeStudent(Student student);
    }

}
