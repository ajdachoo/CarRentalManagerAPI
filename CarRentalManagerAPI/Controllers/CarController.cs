using CarRentalManagerAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalManagerAPI.Controllers
{
    [Route("api/cars")]
    public class CarController : ControllerBase
    {
        private readonly CarRentalManagerDbContext _dbContext;

        public CarController(CarRentalManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetAll()
        {
            var cars = _dbContext.Cars.ToList();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public ActionResult<Car> Get([FromRoute] int id)
        {
            var car = _dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if(car is null)
            {
                return NotFound();
            }

            return Ok(car);
        }
    }
}
