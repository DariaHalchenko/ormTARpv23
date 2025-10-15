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
    public class DocumentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DocumentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Document> GetDocuments()
        {
            return _context.Documents.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Document> GetDocument(int id)
        {
            var document = _context.Documents.Find(id);

            if (document == null)
                return NotFound();

            return document;
        }

        [HttpPost]
        public ActionResult<List<Document>> PostDocument([FromBody] Document document)
        {
            // Проверяем наличие Person
            var personExists = _context.Persons.Any(p => p.Id == document.PersonId);
            if (!personExists)
                return BadRequest($"Person with Id {document.PersonId} does not exist.");

            _context.Documents.Add(document);
            _context.SaveChanges();
            return Ok(_context.Documents.ToList());
        }

        [HttpPut("{id}")]
        public ActionResult<List<Document>> PutDocument(int id, [FromBody] Document updatedDocument)
        {
            var document = _context.Documents.Find(id);
            if (document == null)
                return NotFound();

            // Проверяем наличие Person
            var personExists = _context.Persons.Any(p => p.Id == updatedDocument.PersonId);
            if (!personExists)
                return BadRequest($"Person with Id {updatedDocument.PersonId} does not exist.");

            document.DocumentType = updatedDocument.DocumentType;
            document.Number = updatedDocument.Number;
            document.Country = updatedDocument.Country;
            document.IssueDate = updatedDocument.IssueDate;
            document.ExpiryDate = updatedDocument.ExpiryDate;
            document.IssueCountry = updatedDocument.IssueCountry;
            document.PersonId = updatedDocument.PersonId;

            _context.Documents.Update(document);
            _context.SaveChanges();

            return Ok(_context.Documents.ToList());
        }

        [HttpDelete("{id}")]
        public List<Document> DeleteDocument(int id)
        {
            var document = _context.Documents.Find(id);

            if (document == null)
                return _context.Documents.ToList();

            _context.Documents.Remove(document);
            _context.SaveChanges();

            return _context.Documents.ToList();
        }

        [HttpDelete("/kustutaDocument/{id}")]
        public IActionResult DeleteDocumentAlt(int id)
        {
            var document = _context.Documents.Find(id);

            if (document == null)
                return NotFound();

            _context.Documents.Remove(document);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
