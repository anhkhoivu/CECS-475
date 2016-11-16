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

        public Standard GetStandardByName(string standardName)
        {
            return standardRepository.GetSingle(
                d => d.StandardName.Equals(standardName),
                d => d.Students); //include related employees
        }

        public Standard GetStandardByID(int id)
        {
            return standardRepository.GetSingle(
                d => d.StandardId.Equals(id),
                d => d.Students); //include related students
        }

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
            standardRepository.Remove(departments);
        }

        public IList<Student> GetStudentsByStandardID(int standardID)
        {
            return studentRepository.GetList(e => e.Standard.StandardId.Equals(standardID));
        }

        public IList<Student> getAllStudents()
        {
            return studentRepository.GetAll();
        }

        public Student getStudentByName(string studentName)
        {
            return studentRepository.GetSingle(
                d => d.StudentName.Equals(studentName));
        }

        public Student getStudentByID(int studentId)
        {
            return studentRepository.GetSingle(
                d => d.StudentID.Equals(studentId));
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
            studentRepository.Remove(student);
        }
    }
}
