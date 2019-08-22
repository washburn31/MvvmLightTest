using MvvmLightTest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightTest.Services.Interfaces
{
    public interface IDataAccessService
    {
        List<Employees> GetEmployees();
        int CreateEmployee(Employees Emp);
    }
}
