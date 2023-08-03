using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Models.Car;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalManagerAPI.Controllers
{
    [Route("api/cars")]
    public class CarController : ControllerBase
    {
        private readonly CarRentalManagerDbContext _dbContext;
        private readonly IMapper _mapper;

        public CarController(CarRentalManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreateCar([FromBody] CreateCarDto createCarDto)
        {
            var car = _mapper.Map<Car>(createCarDto);

            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return Created($"api/cars/{car.Id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> GetAll()
        {
            var cars = _dbContext.Cars.ToList();

            var carsDtos = _mapper.Map<List<CarDto>>(cars);

            return Ok(carsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<Car> Get([FromRoute] int id)
        {
            var car = _dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if(car is null)
            {
                return NotFound();
            }

            var carDto = _mapper.Map<CarDto>(car);

            return Ok(carDto);
        }
    }
}
