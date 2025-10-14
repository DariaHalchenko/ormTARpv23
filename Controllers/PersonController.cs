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
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Person> GetPeople()
        {
            return _context.Persons
                .Include(p => p.Document)
                .Include(p => p.ContactData)
                .ToList();
        }

        [HttpPost]
        public List<Person> PostPerson([FromBody] Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
            return _context.Persons
                .Include(p => p.Document)
                .Include(p => p.ContactData)
                .ToList();
        }

        [HttpDelete("{id}")]
        public List<Person> DeletePerson(int id)
        {
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return _context.Persons.ToList();
            }

            _context.Persons.Remove(person);
            _context.SaveChanges();
            return _context.Persons.ToList();
        }

        [HttpDelete("/kustuta2/{id}")]
        public IActionResult DeletePerson2(int id)
        {
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var person = _context.Persons
                .Include(p => p.Document)
                .Include(p => p.ContactData)
                .FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Person>> PutPerson(int id, [FromBody] Person updatedPerson)
        {
            var person = _context.Persons
                .Include(p => p.Document)
                .Include(p => p.ContactData)
                .FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            person.PersonCode = updatedPerson.PersonCode;
            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.Password = updatedPerson.Password;
            person.Admin = updatedPerson.Admin;
            person.DocumentId = updatedPerson.DocumentId;
            person.Document = updatedPerson.Document;
            person.ContactDataId = updatedPerson.ContactDataId;
            person.ContactData = updatedPerson.ContactData;

            _context.Persons.Update(person);
            _context.SaveChanges();

            return Ok(_context.Persons
                .Include(p => p.Document)
                .Include(p => p.ContactData)
                .ToList());
        }
    }
}
