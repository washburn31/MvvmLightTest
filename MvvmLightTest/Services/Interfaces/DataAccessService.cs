using MvvmLightTest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightTest.Services.Interfaces
{
    public class DataAccessService : IDataAccessService
    {
        EmployeeContext context;

        public DataAccessService()
        {
            context = new EmployeeContext();
        }

        public List<Employees> GetEmployees()
        {
            return context.Employees.ToList();
        }

        public int CreateEmployee(Employees emp)
        {
            context.Employees.Add(emp);
            context.SaveChanges();
            return emp.EmpNo;
        }

    }
}
