using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ormTARpv23.Data;
using ormTARpv23.Models;

namespace ormTARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        //Lisa andmebaasi context.
        public ArticleController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Võimalda teha uus päring, mis võimaldab võtta kõiki artikleid
        [HttpGet]
        public List<Article> GetArticles()
        {
            var articles = _context.Articles.ToList();
            return articles;
        }

        //Järgmiseks võimalda teha uus päring, millega on võimalik uut artiklit lisada. Teeme selleks POST päringu.
        [HttpPost]
        public List<Article> PostArtikkel([FromBody] Article artikkel)
        {
            _context.Articles.Add(artikkel);
            _context.SaveChanges();
            return _context.Articles.ToList();
        }

        //Kustutamine - peab kaasa saatma id parameetri
        [HttpDelete("{id}")]
        public List<Article> DeleteArtikkel(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return _context.Articles.ToList();
            }

            _context.Articles.Remove(artikkel);
            _context.SaveChanges();
            return _context.Articles.ToList();
        }

        [HttpDelete("/kustuta2/{id}")]
        public IActionResult DeleteArtikkel2(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(artikkel);
            _context.SaveChanges();
            return NoContent();
        }

        //Võimalda võtta ühte artiklit. Selleks pead API otspunktile lisama samamoodi muutuja
        [HttpGet("{id}")]
        public ActionResult<Article> GetArtikkel(int id)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            return artikkel;
        }

        //PUT päringu abil on võimalik artiklit muuta. Pane tähele, et PATCH kaudu oli võimalik muuta ÜHTE kirjet (näiteks API otspunkt hinna muutmiseks)
        [HttpPut("{id}")]
        public ActionResult<List<Article>> PutArtikkel(int id, [FromBody] Article updatedArtikkel)
        {
            var artikkel = _context.Articles.Find(id);

            if (artikkel == null)
            {
                return NotFound();
            }

            artikkel.Header = updatedArtikkel.Header;
            artikkel.Content = updatedArtikkel.Content;

            _context.Articles.Update(artikkel);
            _context.SaveChanges();

            return Ok(_context.Articles);
        }
    }
}
