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
        public List<Document> PostDocument([FromBody] Document document)
        {
            _context.Documents.Add(document);
            _context.SaveChanges();
            return _context.Documents.ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Document>> PutDocument(int id, [FromBody] Document updatedDocument)
        {
            var document = _context.Documents.Find(id);

            if (document == null)
                return NotFound();

            document.Document_type = updatedDocument.Document_type;
            document.Number = updatedDocument.Number;
            document.Country = updatedDocument.Country;
            document.IssueDate = updatedDocument.IssueDate;
            document.ExpiryDate = updatedDocument.ExpiryDate;
            document.IssueCountry = updatedDocument.IssueCountry;
            document.Person = updatedDocument.Person;

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
