using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _475_Lab_4_Part_3
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository() : base(new SchoolDBEntities())
        {

        }
    }
}
