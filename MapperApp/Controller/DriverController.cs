using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MapperApp.Models;

namespace MapperApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;

        private static List<Driver> drivers = new List<Driver>();
        public DriverController(ILogger<DriverController> logger)
        {
            _logger = logger;
        }

        //GET
        [HttpGet]
        [Route("GetDriver")]
        public IActionResult GetDrivers()
        {
            var allDrivers = drivers.Where(x => x.Status == 1).ToList();
            return Ok(allDrivers);
        }

        //POST
        [HttpPost]
        public IActionResult CreateDriver(Driver data)
        {
            if(ModelState.IsValid)
            {
                drivers.Add(data);
                return CreatedAtAction("GetDrivers", new { data.Id }, data);
            }

            return new JsonResult("Something went Wrong") {StatusCode =500};
        }

        //GET Id
        [HttpGet("{id}")]
        public IActionResult GetDriverId(Guid id)
        {
            var item = drivers.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            return Ok(item.Id);
        }

        //PUT
        [HttpPut("{id}")]
        public IActionResult UpdateDriver(Guid id, Driver data)
        {
            if (id == data.Id)
                return BadRequest();

            var existingDriver = drivers.FirstOrDefault(x => x.Id == id);

            if (existingDriver == null)
                return NotFound();

            existingDriver.DriverNumber = data.DriverNumber;
            existingDriver.FirstName = data.FirstName;
            existingDriver.LastName = data.LastName;
            existingDriver.WorldChampionships = data.WorldChampionships;

            return NoContent();
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteDriver(Guid id)
        {
            var existingDriver = drivers.FirstOrDefault(x => x.Id == id);

            if (existingDriver == null)
                return NotFound();

            existingDriver.Status = 0;

            return NoContent();
        }


    }
}
