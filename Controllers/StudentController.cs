using Microsoft.AspNetCore.Mvc;
using ormTARpv23.Data;
using ormTARpv23.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ormTARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Student
        [HttpGet]
        public ActionResult<List<Student>> GetStudents()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }

        // GET: /Student/{id}
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: /Student
        [HttpPost]
        public ActionResult<List<Student>> PostStudent([FromBody] Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            var students = _context.Students.ToList();
            return Ok(students);
        }

        // PUT: /Student/{id}
        [HttpPut("{id}")]
        public ActionResult<List<Student>> PutStudent(int id, [FromBody] Student updatedStudent)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.StudentCode = updatedStudent.StudentCode;

            _context.Students.Update(student);
            _context.SaveChanges();

            var students = _context.Students.ToList();
            return Ok(students);
        }

        // DELETE: /Student/{id}
        [HttpDelete("{id}")]
        public ActionResult<List<Student>> DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            var students = _context.Students.ToList();
            return Ok(students);
        }
    }
}
