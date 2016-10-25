using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _475_Lab_4_Part_3
{
    public interface IBusinessLayer
    {
        IList<Standard> getAllStandards();
        //Standard GetStandardByID(int id);
        void addStandard(Standard standard);
        void updateStandard(Standard standard);
        void removeStandard(Standard standard);

        IList<Student> getAllStudents();
        void addStudent(Student student);
        void updateStudent(Student student);
        void removeStudent(Student student);
    }
}
