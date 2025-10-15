using Microsoft.AspNetCore.Mvc;
using ormTARpv23.Data;
using ormTARpv23.Models;
using System.Collections.Generic;
using System.Linq;

namespace ormTARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactDataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /ContactData
        [HttpGet]
        public ActionResult<List<ContactData>> GetContactDatas()
        {
            var contactDatas = _context.ContactDatas.ToList();
            return Ok(contactDatas);
        }

        // GET: /ContactData/{id}
        [HttpGet("{id}")]
        public ActionResult<ContactData> GetContactData(int id)
        {
            var contactData = _context.ContactDatas.FirstOrDefault(c => c.Id == id);
            if (contactData == null)
                return NotFound();

            return Ok(contactData);
        }

        // POST: /ContactData
        [HttpPost]
        public ActionResult<List<ContactData>> PostContactData([FromBody] ContactData contactData)
        {
            // Проверка существования Person
            var personExists = _context.Persons.Any(p => p.Id == contactData.PersonId);
            if (!personExists)
                return BadRequest($"Person with Id {contactData.PersonId} does not exist.");

            _context.ContactDatas.Add(contactData);
            _context.SaveChanges();

            var contactDatas = _context.ContactDatas.ToList();
            return Ok(contactDatas);
        }


        // PUT: /ContactData/{id}
        [HttpPut("{id}")]
        public ActionResult<List<ContactData>> PutContactData(int id, [FromBody] ContactData updatedContactData)
        {
            var contactData = _context.ContactDatas.Find(id);
            if (contactData == null)
                return NotFound();

            // Проверка существования Person
            var personExists = _context.Persons.Any(p => p.Id == updatedContactData.PersonId);
            if (!personExists)
                return BadRequest($"Person with Id {updatedContactData.PersonId} does not exist.");

            contactData.Address = updatedContactData.Address;
            contactData.Phone = updatedContactData.Phone;
            contactData.PersonId = updatedContactData.PersonId;

            _context.ContactDatas.Update(contactData);
            _context.SaveChanges();

            return Ok(_context.ContactDatas.ToList());
        }


        // DELETE: /ContactData/{id}
        [HttpDelete("{id}")]
        public ActionResult<List<ContactData>> DeleteContactData(int id)
        {
            var contactData = _context.ContactDatas.Find(id);
            if (contactData == null)
                return NotFound();

            _context.ContactDatas.Remove(contactData);
            _context.SaveChanges();

            var contactDatas = _context.ContactDatas.ToList();
            return Ok(contactDatas);
        }
    }
}
