using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _475_Lab_4_Part_3
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IStandardRepository standardRepository;
        private readonly IStudentRepository studentRepository;

        public BusinessLayer()
        {
            standardRepository = new StandardRepository();
            studentRepository = new StudentRepository();
        }

        public BusinessLayer(IStandardRepository standardRepository,
            IStudentRepository studentRepository)
        {
            this.standardRepository = standardRepository;
            this.studentRepository = studentRepository;
        }
        
        public IList<Standard> getAllStandards()
        {
            return standardRepository.GetAll();
        }
        /*
        public Standard GetDepartmentByName(string standardName)
        {
            return standardRepository.GetSingle(
                d => d.StandardName.Equals(standardName),
                d => d.Students); //include related employees
        }
        */
        public void addStandard(Standard departments)
        {
            standardRepository.Insert(departments);
        }

        public void updateStandard(Standard departments)
        {
            standardRepository.Update(departments);
        }

        public void removeStandard(Standard departments)
        {
            standardRepository.Delete(departments);
        }

        /*
        public IList<Student> GetStudentsByStandardName(string departmentName)
        {
            return studentRepository.GetList(e => e.Standard.Name.Equals(departmentName));
        }
        */

        public IList<Student> getAllStudents()
        {
            return studentRepository.GetAll();
        }

        public void addStudent(Student student)
        {
            studentRepository.Insert(student);
        }

        public void updateStudent(Student student)
        {
            studentRepository.Update(student);
        }

        public void removeStudent(Student student)
        {
            studentRepository.Delete(student);
        }
    }
}
