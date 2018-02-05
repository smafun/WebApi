using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExtendedWebApi.Models;
using ExtendedWebApi.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
#region ServiceController
namespace ExtendedWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ServiceTypesController : Controller
    {
        private readonly DataContext _context;
        #endregion

        public ServiceTypesController(DataContext context)
        {
            _context = context;
        }

        #region snippet_GetAll
        [HttpGet]
        public IEnumerable<ServiceType> GetAll(string searchString)
        {
            return _context.ServiceTypes.ToList();
        }
        #endregion

        #region snippet_GetByID
        [HttpGet("{id}", Name = "GetServiceByID")]
        public IActionResult GetById(long id)
        {
            var item = _context.ServiceTypes.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        #endregion

        #region snippet_Create
        [HttpPost]
        public IActionResult Create([FromBody] ServiceType servicetype)
        {
            if (servicetype == null)
            {
                return BadRequest();
            }

            _context.ServiceTypes.Add(servicetype);
            _context.SaveChanges();
            // return a URI to the newly created resource
            return CreatedAtRoute("GetServiceByID", new { id = servicetype.Id }, servicetype);
        }
        #endregion

        #region snippet_Update
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ServiceType servicetype)
        {
            if (servicetype == null)
            {
                return BadRequest();
            }
            var serviceUpdate = _context.ServiceTypes.FirstOrDefault(t => t.Id == id);
            if (serviceUpdate == null)
            {
                return NotFound();
            }

            serviceUpdate.Name = servicetype.Name;
            _context.ServiceTypes.Update(serviceUpdate);
            _context.SaveChanges();
            return CreatedAtRoute("GetServiceByID", new { id = servicetype.Id }, servicetype);
        }
        #endregion

        #region snippet_Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var service = _context.ServiceTypes.FirstOrDefault(t => t.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            _context.ServiceTypes.Remove(service);
            _context.SaveChanges();
            return new NoContentResult();
        }
        #endregion
    }
}

