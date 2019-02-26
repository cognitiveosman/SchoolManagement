using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolManagementContext _context;
        private readonly IMapper _mapper;

        public CoursesController(SchoolManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {

            var courseModel = _context.Courses.Select(c => _mapper.Map<CourseModel>(c));
            return View(courseModel);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.RecordId == id);

            var courseModel = _mapper.Map<CourseModel>(course);
            if (course == null)
            {
                return NotFound();
            }

            return View(courseModel);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseName,CourseCode,Id")] CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {

                var course = _mapper.Map<Course>(courseModel);
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseModel);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var courseModel = _mapper.Map<CourseModel>(course);
            return View(courseModel);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseName,CourseCode,Id")] CourseModel courseModel)
        {
            if (id != courseModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var course = _mapper.Map<Course>(courseModel);
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(courseModel.Id))
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
            return View(courseModel);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.RecordId == id);

            if (course == null)
            {
                return NotFound();
            }

            var courseModel = _mapper.Map<CourseModel>(course);

            return View(courseModel);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.RecordId == id);
        }
    }
}
