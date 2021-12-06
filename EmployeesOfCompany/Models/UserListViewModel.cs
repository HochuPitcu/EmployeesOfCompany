using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesOfCompany.Models
{
    public class UserListViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
    }
}
