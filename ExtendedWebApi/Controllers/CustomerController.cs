using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExtendedWebApi.Data;
using ExtendedWebApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
#region OrderController
namespace ExtendedWebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly DataContext _context;
        #endregion

        public CustomerController(DataContext context)
        {
            _context = context;
        }

        #region snippet_GetAll
        [HttpGet]
        public IEnumerable<Customer> GetAll(string searchString)
        {

            return _context.Customers.ToList();
        }
        #endregion

        #region snippet_GetByID
        [HttpGet("{id}", Name = "GetCustomById")]
        public IActionResult GetById(long id)
        {
            var item = _context.Customers.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        #endregion

        #region snippet_Search
        [HttpGet("search/{searchString}", Name = "SearchByName")]
        public IEnumerable<Customer> Search(string searchString)
        {
            var customers = from m in _context.Customers
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.Name.Contains(searchString));
            }

            return customers.ToList();
        }
        #endregion

        #region snippet_Create
        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();
            // return a URI to the newly created resource
            return CreatedAtRoute("GetCustomByID", new { id = customer.Id }, customer);
        }
        #endregion

        #region snippet_Update
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Customer customer)
        {
            if (customer == null )
            {
                return BadRequest();
            }

            var customerUpdate = _context.Customers.FirstOrDefault(t => t.Id == id);
            if (customerUpdate == null)
            {
                return NotFound();
            }

            customerUpdate.Name = customer.Name;
            customerUpdate.Phone = customer.Phone;
            customerUpdate.Email = customer.Email;

            _context.Customers.Update(customerUpdate);
            _context.SaveChanges();
            return CreatedAtRoute("GetCustomByID", new { id = customer.Id }, customer);
        }
        #endregion

        #region snippet_Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var customer = _context.Customers.FirstOrDefault(t => t.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return new NoContentResult();
        }
        #endregion
    }
}
