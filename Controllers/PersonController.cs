using Microsoft.AspNetCore.Mvc;
using ormTARpv23.Data;
using ormTARpv23.Models;
using System.Collections.Generic;
using System.Linq;

namespace ormTARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Person
        [HttpGet]
        public ActionResult<List<Person>> GetPeople()
        {
            return Ok(_context.Persons.ToList());
        }

        // GET: /Person/{id}
        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var person = _context.Persons.FirstOrDefault(p => p.Id == id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        // POST: /Person
        [HttpPost]
        public ActionResult<List<Person>> PostPerson([FromBody] Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();

            return Ok(_context.Persons.ToList());
        }

        // PUT: /Person/{id}
        [HttpPut("{id}")]
        public ActionResult<List<Person>> PutPerson(int id, [FromBody] Person updatedPerson)
        {
            var person = _context.Persons.FirstOrDefault(p => p.Id == id);

            if (person == null)
                return NotFound();

            person.PersonCode = updatedPerson.PersonCode;
            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.Password = updatedPerson.Password;
            person.Admin = updatedPerson.Admin;

            _context.Persons.Update(person);
            _context.SaveChanges();

            return Ok(_context.Persons.ToList());
        }

        // DELETE: /Person/{id}
        [HttpDelete("{id}")]
        public ActionResult<List<Person>> DeletePerson(int id)
        {
            var person = _context.Persons.FirstOrDefault(p => p.Id == id);

            if (person == null)
                return NotFound();

            _context.Persons.Remove(person);
            _context.SaveChanges();

            return Ok(_context.Persons.ToList());
        }
    }
}
