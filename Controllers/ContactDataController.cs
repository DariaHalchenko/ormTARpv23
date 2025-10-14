using Microsoft.AspNetCore.Mvc;
using ormTARpv23.Data;
using ormTARpv23.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public List<ContactData> GetContactDatas()
        {
            return _context.ContactDatas.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ContactData> GetContactData(int id)
        {
            var contactData = _context.ContactDatas.Find(id);

            if (contactData == null)
                return NotFound();

            return contactData;
        }

        [HttpPost]
        public List<ContactData> PostContactData([FromBody] ContactData contactData)
        {
            _context.ContactDatas.Add(contactData);
            _context.SaveChanges();
            return _context.ContactDatas.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<ContactData>> PutContactData(int id, [FromBody] ContactData updatedContactData)
        {
            var contactData = _context.ContactDatas.Find(id);

            if (contactData == null)
                return NotFound();

            contactData.Address = updatedContactData.Address;
            contactData.Phone = updatedContactData.Phone;
            contactData.Person = updatedContactData.Person;

            _context.ContactDatas.Update(contactData);
            _context.SaveChanges();

            return Ok(_context.ContactDatas.ToList());
        }

        [HttpDelete("{id}")]
        public List<ContactData> DeleteContactData(int id)
        {
            var contactData = _context.ContactDatas.Find(id);

            if (contactData == null)
                return _context.ContactDatas.ToList();

            _context.ContactDatas.Remove(contactData);
            _context.SaveChanges();

            return _context.ContactDatas.ToList();
        }

        [HttpDelete("/kustutaContactData/{id}")]
        public IActionResult DeleteContactDataAlt(int id)
        {
            var contactData = _context.ContactDatas.Find(id);

            if (contactData == null)
                return NotFound();

            _context.ContactDatas.Remove(contactData);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
