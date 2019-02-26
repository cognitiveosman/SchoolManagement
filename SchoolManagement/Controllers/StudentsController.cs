using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolManagementContext _context;
        private readonly IMapper _mapper;


        public StudentsController(SchoolManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // GET: Students
        public async Task<IActionResult> Index()
        {

            var studentsListTemp = _context.Students.Include(s => s.StudentCourses).ThenInclude(c => c.Course);
            var studentsList = studentsListTemp.Select(s => _mapper.Map<StudentModel>(s)).ToList();
            return View(studentsList);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s => s.StudentCourses).ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(m => m.RecordId == id);

            var studentModel = _mapper.Map<StudentModel>(student);
            if (student == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        // GET: Students/Create
        public IActionResult Create()
        {

            var studentModel = new StudentModel();
            populateCourses(studentModel);
            return View(studentModel);
        }

        private void populateCourses(StudentModel studentModel)
        {
            studentModel.AllCourses = _context.Courses.Select(c => new SelectListItem()
            { Text = c.CourseName, Value = c.CourseCode }).ToList();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,PersonNumber,Courses,Id")] StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {

                var student = _mapper.Map<Student>(studentModel);

                var selectedCourses =
                    studentModel.Courses.Select(c => _context.Courses.FirstOrDefault(cc => cc.CourseCode.Equals(c))).ToList();
                student.StudentCourses = new List<StudentCourse>();
                var selectedStudentCourse = selectedCourses.Select(c => new StudentCourse() { Course = c });
                foreach (var studentCourse in selectedStudentCourse)
                {
                    studentCourse.Student = student;
                    student.StudentCourses.Add(studentCourse);


                }


                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _context.Students.Include(s => s.StudentCourses).ThenInclude(c => c.Course).FirstOrDefault(s => s.RecordId == id);

            var studentModel = _mapper.Map<StudentModel>(student);

            populateCourses(studentModel);

            if (student == null)
            {
                return NotFound();
            }
            return View(studentModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,PersonNumber,Courses,Id")] StudentModel studentModel)
        {
            if (id != studentModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var student = _context.Students.Include(s => s.StudentCourses).ThenInclude(c => c.Course).FirstOrDefault(s => s.RecordId == id);

                    if (student == null)
                    {
                        return NotFound();

                    }
                    // var student = _mapper.Map<Student>(studentModel);

                    var selectedCourses =
                    studentModel.Courses.Select(c => _context.Courses.FirstOrDefault(cc => cc.CourseCode.Equals(c))).ToList();
                    student.StudentCourses = new List<StudentCourse>();
                    var selectedStudentCourse = selectedCourses.Select(c => new StudentCourse() { Course = c });

                    foreach (var studentCourse in selectedStudentCourse)
                    {
                        studentCourse.Student = student;
                        student.StudentCourses.Add(studentCourse);


                    }
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(studentModel.Id))
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
            return View(studentModel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.RecordId == id);
            var studentModel = _mapper.Map<StudentModel>(student);
            if (student == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.RecordId == id);
        }
    }
}
