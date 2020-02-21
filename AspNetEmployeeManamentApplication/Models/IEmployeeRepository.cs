using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetEmployeeManamentApplication.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int ID);
        IEnumerable<Employee> GetAllEmployees();

        Employee Add(Employee employee);

        Employee Delete(int ID);
        Employee Update(Employee EmployeeChanges);
    }
}
