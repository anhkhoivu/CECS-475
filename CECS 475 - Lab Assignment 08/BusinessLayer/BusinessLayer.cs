using _475_Lab_4_Part_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BusinessLayer 
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
        /*
        public IList<Standard> GetAllDepartments()
        {
            return standardRepository.GetAll();
        }
        
        public Standard GetDepartmentByName(string standardName)
        {
            return standardRepository.GetSingle(
                d => d.StandardName.Equals(standardName),
                d => d.Students); //include related employees
        }
        */
        public void AddStandard(Standard departments)
        {
            standardRepository.Insert(departments);
        }

        public void UpdateStandard(Standard departments)
        {
            standardRepository.Update(departments);
        }

        public void RemoveStandard(Standard departments)
        {
            standardRepository.Delete(departments);
        }
        /*
        public IList<Student> GetStudentsByStandardName(string departmentName)
        {
            return studentRepository.GetList(e => e.Standard.Name.Equals(departmentName));
        }
        */
        public void AddStudent(Student student)
        {
            studentRepository.Insert(student);
        }

        public void UpdateEmploee(Student student)
        {
            studentRepository.Update(student);
        }

        public void RemoveEmployee(Student student)
        {
            studentRepository.Delete(student);
        }
    }
}
