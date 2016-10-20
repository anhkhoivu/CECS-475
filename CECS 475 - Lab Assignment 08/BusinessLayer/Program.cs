using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IBusinessLayer
    {
        IList<Department> GetAllDepartments();
        Department GetDepartmentByName(string departmentName);
        void AddDepartment(params Department[] departments);
        void UpdateDepartment(params Department[] departments);
        void RemoveDepartment(params Department[] departments);

        IList<Employee> GetEmployeesByDepartmentName(string departmentName);
        void AddEmployee(Employee employee);
        void UpdateEmploee(Employee employee);
        void RemoveEmployee(Employee employee);
    }

    public class BuinessLayer : IBusinessLayer
    {
        private readonly IDepartmentRepository _deptRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BuinessLayer()
        {
            _deptRepository = new DepartmentRepository();
            _employeeRepository = new EmployeeRepository();
        }

        public BuinessLayer(IDepartmentRepository deptRepository,
            IEmployeeRepository employeeRepository)
        {
            _deptRepository = deptRepository;
            _employeeRepository = employeeRepository;
        }

        public IList<Department> GetAllDepartments()
        {
            return _deptRepository.GetAll();
        }

        public Department GetDepartmentByName(string departmentName)
        {
            return _deptRepository.GetSingle(
                d => d.Name.Equals(departmentName),
                d => d.Employees); //include related employees
        }

        public void AddDepartment(params Department[] departments)
        {
            /* Validation and error handling omitted */
            _deptRepository.Add(departments);
        }

        public void UpdateDepartment(params Department[] departments)
        {
            /* Validation and error handling omitted */
            _deptRepository.Update(departments);
        }

        public void RemoveDepartment(params Department[] departments)
        {
            /* Validation and error handling omitted */
            _deptRepository.Remove(departments);
        }

        public IList<Employee> GetEmployeesByDepartmentName(string departmentName)
        {
            return _employeeRepository.GetList(e => e.Department.Name.Equals(departmentName));
        }

        public void AddEmployee(Employee employee)
        {
            /* Validation and error handling omitted */
            _employeeRepository.Add(employee);
        }

        public void UpdateEmploee(Employee employee)
        {
            /* Validation and error handling omitted */
            _employeeRepository.Update(employee);
        }

        public void RemoveEmployee(Employee employee)
        {
            /* Validation and error handling omitted */
            _employeeRepository.Remove(employee);
        }
    }
}
