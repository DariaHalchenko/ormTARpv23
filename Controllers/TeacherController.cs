using Microsoft.AspNetCore.Mvc;
using ormTARpv23.Data;
using ormTARpv23.Models;
using System.Collections.Generic;
using System.Linq;

namespace ormTARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Teacher
        [HttpGet]
        public ActionResult<List<Teacher>> GetTeachers()
        {
            var teachers = _context.Teachers.ToList();
            return Ok(teachers);
        }

        // GET: /Teacher/{id}
        [HttpGet("{id}")]
        public ActionResult<Teacher> GetTeacher(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

        // POST: /Teacher
        [HttpPost]
        public ActionResult<List<Teacher>> PostTeacher([FromBody] Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();

            var teachers = _context.Teachers.ToList();
            return Ok(teachers);
        }

        // PUT: /Teacher/{id}
        [HttpPut("{id}")]
        public ActionResult<List<Teacher>> PutTeacher(int id, [FromBody] Teacher updatedTeacher)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
                return NotFound();

            teacher.FirstName = updatedTeacher.FirstName;
            teacher.LastName = updatedTeacher.LastName;
            teacher.Email = updatedTeacher.Email;

            _context.Teachers.Update(teacher);
            _context.SaveChanges();

            var teachers = _context.Teachers.ToList();
            return Ok(teachers);
        }

        // DELETE: /Teacher/{id}
        [HttpDelete("{id}")]
        public ActionResult<List<Teacher>> DeleteTeacher(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
            {
                return _context.Teachers.ToList();
            }

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();

            var teachers = _context.Teachers.ToList();
            return Ok(teachers);
        }
    }
}
