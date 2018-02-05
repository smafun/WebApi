using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExtendedWebApi.Models;
using ExtendedWebApi.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
#region OrderController
namespace ExtendedWebApi.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly DataContext _context;
        #endregion

        public OrderController(DataContext context)
        {
            _context = context;
        }

        #region snippet_GetAll
        [HttpGet]
        public IEnumerable<Order> GetAll(string searchString)
        {
            return _context.Orders.ToList();
        }
        #endregion

        #region snippet_GetByID
        [HttpGet("{id}", Name = "GetOrderByID")]
        public IActionResult GetById(long id)
        {
            var item = _context.Orders.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        #endregion


        #region snippet_Create
        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
            // return a URI to the newly created resource
            return CreatedAtRoute("GetOrderByID", new { id = order.Id }, order);
        }
        #endregion

        #region snippet_Update
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Order order)
        {
            if (order == null || order.Id != id)
            {
                return BadRequest();
            }

            var orderUpdate = _context.Orders.FirstOrDefault(t => t.Id == id);
            if (orderUpdate == null)
            {
                return NotFound();
            }

            orderUpdate.AddressFrom = order.AddressFrom;
            orderUpdate.AddressTo = order.AddressTo;
            orderUpdate.Date = order.Date;
            orderUpdate.TxtField = order.TxtField;

            _context.Orders.Update(orderUpdate);
            _context.SaveChanges();
            return new NoContentResult();
        }
        #endregion

        #region snippet_Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var order = _context.Orders.FirstOrDefault(t => t.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return new NoContentResult();
        }
        #endregion
    }
}
