using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers
{
    public class ClassRoomsController : Controller
    {
        private readonly SchoolManagementContext _context;
        private readonly IMapper _mapper;


        public ClassRoomsController(SchoolManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // GET: ClassRooms
        public async Task<IActionResult> Index()
        {

            var classroomModel = _context.ClassRooms.Select(cr => _mapper.Map<ClassRoomModel>(cr));
            return View(classroomModel);
        }

        // GET: ClassRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classRoom = await _context.ClassRooms
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (classRoom == null)
            {
                return NotFound();
            }

            var classroomModel = _mapper.Map<ClassRoomModel>(classRoom);

            return View(classroomModel);
        }

        // GET: ClassRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Id")] ClassRoomModel classroomModel)
        {
            if (ModelState.IsValid)
            {
                var classRoom = _mapper.Map<ClassRoom>(classroomModel);
                _context.Add(classRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classroomModel);
        }

        // GET: ClassRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classRoom = await _context.ClassRooms.FindAsync(id);
            if (classRoom == null)
            {
                return NotFound();
            }

            var classroomModel = _mapper.Map<ClassRoomModel>(classRoom);
            return View(classroomModel);
        }

        // POST: ClassRooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Code,Id")] ClassRoomModel classroomModel)
        {
            if (id != classroomModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var classRoom = _mapper.Map<ClassRoom>(classroomModel);
                    _context.Update(classRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassRoomExists(classroomModel.Id))
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
            return View(classroomModel);
        }

        // GET: ClassRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classRoom = await _context.ClassRooms
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (classRoom == null)
            {
                return NotFound();
            }

            var classroomModel = _mapper.Map<ClassRoomModel>(classRoom);

            return View(classroomModel);
        }

        // POST: ClassRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classRoom = await _context.ClassRooms.FindAsync(id);
            _context.ClassRooms.Remove(classRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassRoomExists(int id)
        {
            return _context.ClassRooms.Any(e => e.RecordId == id);
        }
    }
}
