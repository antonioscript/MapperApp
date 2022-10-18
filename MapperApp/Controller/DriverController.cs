using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MapperApp.Models;
using MapperApp.DTOs.Incoming;
using AutoMapper;
using MapperApp.DTOs.Outgoing;

namespace MapperApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;

        private static List<Driver> drivers = new List<Driver>();
        private readonly IMapper _mapper;
        public DriverController(ILogger<DriverController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        //GET
        [HttpGet]
        [Route("GetDriver")]
        public IActionResult GetDrivers()
        {
            var allDrivers = drivers.Where(x => x.Status == 1).ToList();

            var _drivers = _mapper.Map<IEnumerable<DriverDto>>(allDrivers);
            return Ok(_drivers);
        }

        //POST
        [HttpPost]
        public IActionResult CreateDriver(DriverForCreationDto data)
        {
            if(ModelState.IsValid)
            {
                var _driver = _mapper.Map<Driver>(data);

                drivers.Add(_driver);

                var newDriver = _mapper.Map<DriverDto>(_driver);
                return CreatedAtAction("GetDrivers", new { _driver.Id }, newDriver);
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
