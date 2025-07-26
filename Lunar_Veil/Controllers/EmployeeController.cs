using Lunar_Veil.Data;
using Microsoft.AspNetCore.Mvc;
using FuzzySharp;

namespace Lunar_Veil.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly _22bitv02EmployeeContext _context;

        public EmployeeController(_22bitv02EmployeeContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchTerm)
        {
            var employees = _context.Employees.ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                employees = employees.Where(e =>
                    Fuzz.PartialRatio(e.EmployeeName, searchTerm) > 70 ||
                    Fuzz.PartialRatio(e.Email, searchTerm) > 70 ||
                    Fuzz.PartialRatio(e.Phone, searchTerm) > 70).ToList();
            }
            return View();
        }
    }
}
