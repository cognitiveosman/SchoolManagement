using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly SchoolManagementContext _context;
        private readonly IMapper _mapper;

        public SchedulesController(SchoolManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var scheduleModel = _context.Schedule.Include(c => c.ClassRoom).Include(c => c.Course).Select(s => _mapper.Map<ScheduleModel>(s)).ToList();

            return View(scheduleModel);
        }

        private void populateClassRoomsAndCourses(ScheduleModel scheduleModel)
        {
            scheduleModel.AllClassRooms = _context.ClassRooms.Select(cr => new SelectListItem()
            { Value = cr.Code, Text = cr.Code }).ToList();

            scheduleModel.AllCourses = _context.Courses.Select(cr => new SelectListItem()
            { Value = cr.CourseCode, Text = cr.CourseName }).ToList();


        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.Include(c => c.ClassRoom).Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.RecordId == id);

            if (schedule == null)
            {
                return NotFound();
            }

            var scheduleModel = _mapper.Map<ScheduleModel>(schedule);

            return View(scheduleModel);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            var scheduleModel = new ScheduleModel();
            populateClassRoomsAndCourses(scheduleModel);
            return View(scheduleModel);
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartTime,EndTime,ClassRoom,CourseCode,Id")] ScheduleModel scheduleModel)
        {
            if (ModelState.IsValid)
            {

                var schedule = _mapper.Map<Schedule>(scheduleModel);

                //get corresponding course
                var course = _context.Courses.FirstOrDefault(c => c.CourseCode.Equals(scheduleModel.CourseCode));
                schedule.Course = course;
                //get corresponding class-room
                var classroom = _context.ClassRooms.FirstOrDefault(c => c.Code.Equals(scheduleModel.ClassRoom));
                schedule.ClassRoom = classroom;

                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scheduleModel);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = _context.Schedule.Include(c => c.ClassRoom).Include(c => c.Course).FirstOrDefault(s => s.RecordId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            var scheduleModel = _mapper.Map<ScheduleModel>(schedule);
            populateClassRoomsAndCourses(scheduleModel);

            return View(scheduleModel);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StartTime,EndTime,ClassRoom,CourseCode,Id")] ScheduleModel scheduleModel)
        {
            if (id != scheduleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var schedule = _mapper.Map<Schedule>(scheduleModel);

                    //get corresponding course
                    var course = _context.Courses.FirstOrDefault(c => c.CourseCode.Equals(scheduleModel.CourseCode));
                    schedule.Course = course;
                    //get corresponding class-room
                    var classroom = _context.ClassRooms.FirstOrDefault(c => c.Code.Equals(scheduleModel.ClassRoom));
                    schedule.ClassRoom = classroom;

                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(scheduleModel.Id))
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

            return View(scheduleModel);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (schedule == null)
            {
                return NotFound();
            }
            var scheduleModel = _mapper.Map<ScheduleModel>(schedule);

            return View(scheduleModel);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            _context.Schedule.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.RecordId == id);
        }


    }
}
