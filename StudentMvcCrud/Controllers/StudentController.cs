using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMvcCrud.Models;

namespace StudentMvcCrud.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _studentContext;

        public StudentController(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _studentContext.Students.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                NotFound();
            }

            var student = await _studentContext.Students.FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                NotFound();
            }

            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Age,GPA")] Student student)
        {
            if (ModelState.IsValid)
            {
                _studentContext.Add(student);
                await _studentContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                NotFound();
            }

            var student = await _studentContext.Students.FindAsync(id);

            if (student == null)
            {
                NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Age,GPA")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _studentContext.Update(student);
                    await _studentContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentContext.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _studentContext.Students.FindAsync(id);
            if (student != null)
            {
                _studentContext.Students.Remove(student);
            }

            await _studentContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _studentContext.Students.Any(student => student.Id == id);
        }
    }
}
