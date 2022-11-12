using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Api.Context;
using School.Entities;

namespace School.Api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CoursesStudentsController : ControllerBase
   {
      private readonly SchoolDBContext _context;

      public CoursesStudentsController(SchoolDBContext context)
      {
         _context = context;
      }

      // GET: api/CoursesStudents
      [HttpGet]
      public async Task<ActionResult<IEnumerable<CoursesStudent>>> GetCoursesStudents()
      {
         if (_context.CoursesStudents == null)
         {
            return NotFound();
         }
         return await _context.CoursesStudents.ToListAsync();
      }

      // GET: api/CoursesStudents/5
      [HttpGet("{id}")]
      public async Task<ActionResult<CoursesStudent>> GetCoursesStudent(int id)
      {
         if (_context.CoursesStudents == null)
         {
            return NotFound();
         }
         var coursesStudent = await _context.CoursesStudents.FindAsync(id);

         if (coursesStudent == null)
         {
            return NotFound();
         }

         return coursesStudent;
      }

      // PUT: api/CoursesStudents/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutCoursesStudent(int id, CoursesStudent coursesStudent)
      {
         if (id != coursesStudent.CourseId)
         {
            return BadRequest();
         }

         _context.Entry(coursesStudent).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!CoursesStudentExists(id))
            {
               return NotFound();
            }
            else
            {
               throw;
            }
         }

         return NoContent();
      }

      // POST: api/CoursesStudents
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<CoursesStudent>> PostCoursesStudent(CoursesStudent coursesStudent)
      {
         if (_context.CoursesStudents == null)
         {
            return Problem("Entity set 'SchoolDBContext.CoursesStudents'  is null.");
         }
         _context.CoursesStudents.Add(coursesStudent);
         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateException)
         {
            if (CoursesStudentExists(coursesStudent.CourseId))
            {
               return Conflict();
            }
            else
            {
               throw;
            }
         }

         return CreatedAtAction("GetCoursesStudent", new { id = coursesStudent.CourseId }, coursesStudent);
      }

      // DELETE: api/CoursesStudents/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteCoursesStudent(int id)
      {
         if (_context.CoursesStudents == null)
         {
            return NotFound();
         }
         var coursesStudent = await _context.CoursesStudents.FindAsync(id);
         if (coursesStudent == null)
         {
            return NotFound();
         }

         _context.CoursesStudents.Remove(coursesStudent);
         await _context.SaveChangesAsync();

         return NoContent();
      }

      private bool CoursesStudentExists(int id)
      {
         return (_context.CoursesStudents?.Any(e => e.CourseId == id)).GetValueOrDefault();
      }
   }
}