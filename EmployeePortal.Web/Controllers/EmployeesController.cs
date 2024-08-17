using EmployeePortal.Web.Data;
using EmployeePortal.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
namespace EmployeePortal.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EFDbContext context;

        public EmployeesController(EFDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var cities = new SelectList(context.Cities.Select(c => c.Name).OrderBy(c => c));
            ViewBag.Cities = cities;
            return View();
        }

        [HttpGet]
        [Route("/Employees/List", Name = "List")]
        public async Task<IActionResult> List(string searchEmployee)
        {
            var employee = context.Employees.Select(e => e);
            if (!String.IsNullOrEmpty(searchEmployee))
            {
                employee = employee.Where(e =>
                e.Name.Contains(searchEmployee) ||
                e.CityName.Contains(searchEmployee) ||
                e.Code.Contains(searchEmployee));

                return View(await employee.AsNoTracking().ToListAsync());
            }
            else
            {
                var selectCity = new SelectList(context.Cities.Select(c => c.Name).OrderBy(c => c));
                ViewBag.Cities = selectCity;
                var employees = await context.Employees.ToListAsync();

                return View(employees);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            var cities = new SelectList(context.Cities.Select(c => c.Name).OrderBy(c => c));
            ViewBag.Cities = cities;
            try
            {
                if (ModelState.IsValid)
                {
                    context.Add(employee);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var selectCity = new SelectList(context.Cities.Select(c => c.Name).OrderBy(c => c));
            ViewBag.Cities = selectCity;
            var employee = context.Employees.Find(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var employeeToUpdate = await context.Employees.FirstOrDefaultAsync(e => e.ID == id);
            if (await TryUpdateModelAsync<Employee>(
                employeeToUpdate,
                "",
                e => e.Name, e => e.Code, e => e.CityName))
            {
                try
                {
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            return View(employeeToUpdate);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id is null)
            {
                return NotFound();
            }

            var employee = await context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == id);
            if (employee is null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed.";
            }

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await context.Employees.FindAsync(id);
            if (employee is null)
            {
                return RedirectToAction(nameof(List));
            }
            try
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}
