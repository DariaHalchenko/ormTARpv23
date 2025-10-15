using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ormTARpv23.Data;
using ormTARpv23.Models;
using Microsoft.EntityFrameworkCore;

namespace ormTARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            return _context.Orders
                .Include(o => o.CartProduct)
                .Include(o => o.Person)
                .ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Orders
                .Include(o => o.CartProduct)
                .Include(o => o.Person)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            return order;
        }

        [HttpPost]
        public List<Order> PostOrder([FromBody] Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return _context.Orders
                .Include(o => o.CartProduct)
                .Include(o => o.Person)
                .ToList();
        }

        [HttpPut("{id}")]
        public ActionResult<List<Order>> PutOrder(int id, [FromBody] Order updatedOrder)
        {
            var order = _context.Orders
                .Include(o => o.CartProduct)
                .Include(o => o.Person)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            order.created = updatedOrder.created;
            order.TotalSum = updatedOrder.TotalSum;
            order.Paid = updatedOrder.Paid;
            order.Person = updatedOrder.Person;
            order.CartProduct = updatedOrder.CartProduct;

            _context.Orders.Update(order);
            _context.SaveChanges();

            return Ok(_context.Orders
                .Include(o => o.CartProduct)
                .Include(o => o.Person)
                .ToList());
        }

        [HttpDelete("{id}")]
        public List<Order> DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
                return _context.Orders.ToList();

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return _context.Orders.ToList();
        }
    }
}
