using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetEmployeeManamentApplication.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDBContext context;

        public SQLEmployeeRepository(AppDBContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int ID)
        {
            Employee employee = context.Employees.FirstOrDefault(e => e.ID == ID);
            if(employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
           return context.Employees;
        }

        public Employee GetEmployee(int ID)
        {
            Employee employee = context.Employees.FirstOrDefault(e => e.ID == ID);
            return employee;
        }

        public Employee Update(Employee EmployeeChanges)
        {
            var employee = context.Employees.Attach(EmployeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return EmployeeChanges;
        }
    }
}
