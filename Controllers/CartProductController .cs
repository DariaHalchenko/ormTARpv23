using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ormTARpv23.Data;
using ormTARpv23.Models;

namespace ormTARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /CartProduct
        [HttpGet]
        public List<CartProduct> GetCartProducts()
        {
            return _context.CartProducts.ToList();
        }

        // GET: /CartProduct/{id}
        [HttpGet("{id}")]
        public ActionResult<CartProduct> GetCartProduct(int id)
        {
            var cartProduct = _context.CartProducts.Find(id);
            if (cartProduct == null)
            {
                return NotFound();
            }
            return cartProduct;
        }

        // POST: /CartProduct
        [HttpPost]
        public ActionResult<List<CartProduct>> PostCartProduct([FromBody] CartProduct cartProduct)
        {
            _context.CartProducts.Add(cartProduct);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }

        // PUT: /CartProduct/{id}
        [HttpPut("{id}")]
        public ActionResult<List<CartProduct>> PutCartProduct(int id, [FromBody] CartProduct updatedCartProduct)
        {
            var cartProduct = _context.CartProducts.Find(id);
            if (cartProduct == null)
            {
                return NotFound();
            }

            cartProduct.ProductId = updatedCartProduct.ProductId;
            cartProduct.Quantity = updatedCartProduct.Quantity;

            _context.CartProducts.Update(cartProduct);
            _context.SaveChanges();

            return _context.CartProducts.ToList();
        }

        // DELETE: /CartProduct/{id}
        [HttpDelete("{id}")]
        public ActionResult<List<CartProduct>> DeleteCartProduct(int id)
        {
            var cartProduct = _context.CartProducts.Find(id);
            if (cartProduct == null)
            {
                return _context.CartProducts.ToList();
            }

            _context.CartProducts.Remove(cartProduct);
            _context.SaveChanges();

            return _context.CartProducts.ToList();
        }
    }
}