using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetEmployeeManamentApplication.Models
{
    public class MokeEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MokeEmployeeRepository()
        {
            _employeeList = new List<Employee>()
                {
                new Employee() { ID = 1, Name = "Mary", Department =Dept.HR, Email = "mary@pragimtech.com" },
                new Employee() { ID = 2, Name = "John", Department = Dept.IT, Email = "john@pragimtech.com" },
                new Employee() { ID = 3, Name = "Sam", Department = Dept.Payroll, Email = "sam@pragimtech.com" },
                };
        }

        public Employee Add(Employee employee)
        {
            employee.ID = _employeeList.Max(e=> e.ID) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int ID)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.ID == ID);
            if(employee!=null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int ID)
        {
            return _employeeList.FirstOrDefault(e => e.ID == ID);
        }

        public Employee Update(Employee EmployeeChanges)
        {

            Employee employee = _employeeList.FirstOrDefault(e => e.ID == EmployeeChanges.ID);
            if (employee != null)
            {
                employee.Name = EmployeeChanges.Name;
                employee.Email = EmployeeChanges.Email;
                employee.Department = EmployeeChanges.Department;
            }
            return employee;
        }
    }
}
