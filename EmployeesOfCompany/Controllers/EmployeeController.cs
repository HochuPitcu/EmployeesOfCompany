using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesOfCompany.Models;

namespace EmployeesOfCompany.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;
        static List<Employee> emp = new List<Employee>();
        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        public ActionResult Employee(string department, string fullname, DateTime birthDate, DateTime employmentDate, int? salary, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Employee> employees = _context.Employees;
            ViewData["DepartmentSort"] = sortOrder == SortState.DepartmentaAsc ? SortState.DepartmentDesc : SortState.DepartmentaAsc;
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["DateSort"] = sortOrder == SortState.BirthAsc ? SortState.BirthDesc : SortState.BirthAsc;
            ViewData["EmploymentSort"] = sortOrder == SortState.EmploymentAsc ? SortState.EmploymentDesc : SortState.EmploymentAsc;
            ViewData["SalarySort"] = sortOrder == SortState.SalaryAsc ? SortState.SalaryDesc : SortState.SalaryAsc;

            if (!String.IsNullOrEmpty(department))
            {
                employees = employees.Where(p => p.Department.Contains(department));
            }
            if (!String.IsNullOrEmpty(fullname))
            {
                employees = employees.Where(p => p.FullName.Contains(fullname));
            }
            if (birthDate != DateTime.MinValue)
            {
                employees = employees.Where(p => p.DateOfBirth == birthDate);
            }
            if (employmentDate != DateTime.MinValue)
            {
                employees = employees.Where(p => p.DateOfEmployment == employmentDate);
            }
            if (salary != 0 && salary != null)
            {
                employees = employees.Where(p => p.Salary == salary);
            }
            employees = sortOrder switch
            {
                SortState.DepartmentDesc => employees.OrderByDescending(s => s.Department),
                SortState.NameAsc => employees.OrderBy(s => s.FullName),
                SortState.NameDesc => employees.OrderByDescending(s => s.FullName),
                SortState.BirthAsc => employees.OrderBy(s => s.DateOfBirth),
                SortState.BirthDesc => employees.OrderByDescending(s => s.DateOfBirth),
                SortState.EmploymentAsc => employees.OrderBy(s => s.DateOfEmployment),
                SortState.EmploymentDesc => employees.OrderByDescending(s => s.DateOfEmployment),
                SortState.SalaryAsc => employees.OrderBy(s => s.Salary),
                SortState.SalaryDesc => employees.OrderByDescending(s => s.Salary),
                _ => employees.OrderBy(s => s.Department),
            };

            UserListViewModel viewModel = new UserListViewModel
            {
                Employees = employees.ToList(),
            };
            return View(viewModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return PartialView(new Employee());
            }
            else
                return PartialView(_context.Employees.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("EmployeeId,Department,FullName,DateOfBirth,DateOfEmployment,Salary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.EmployeeId == 0)
                    _context.Add(employee);
                else
                    _context.Update(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Employee));
            }
            return View(employee);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var emploee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(emploee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
