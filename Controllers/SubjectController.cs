using Microsoft.AspNetCore.Mvc;
using ormTARpv23.Data;
using ormTARpv23.Models;
using System.Collections.Generic;
using System.Linq;

namespace ormTARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Subject
        [HttpGet]
        public ActionResult<List<Subject>> GetSubjects()
        {
            return Ok(_context.Subjects.ToList());
        }

        // GET: /Subject/{id}
        [HttpGet("{id}")]
        public ActionResult<Subject> GetSubject(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null) return NotFound();
            return Ok(subject);
        }

        // POST: /Subject
        [HttpPost]
        public ActionResult<List<Subject>> PostSubject([FromBody] Subject subject)
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();
            return Ok(_context.Subjects.ToList());
        }

        // PUT: /Subject/{id}
        [HttpPut("{id}")]
        public ActionResult<List<Subject>> PutSubject(int id, [FromBody] Subject updatedSubject)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null) return NotFound();

            subject.Name = updatedSubject.Name;
            subject.Credits = updatedSubject.Credits;
            subject.TeacherId = updatedSubject.TeacherId;
            subject.StudentId = updatedSubject.StudentId;

            _context.Subjects.Update(subject);
            _context.SaveChanges();

            return Ok(_context.Subjects.ToList());
        }

        // DELETE: /Subject/{id}
        [HttpDelete("{id}")]
        public ActionResult<List<Subject>> DeleteSubject(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null) return Ok(_context.Subjects.ToList());

            _context.Subjects.Remove(subject);
            _context.SaveChanges();

            return Ok(_context.Subjects.ToList());
        }
    }
}
