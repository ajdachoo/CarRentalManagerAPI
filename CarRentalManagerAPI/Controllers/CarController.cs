using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Models.Car;
using CarRentalManagerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalManagerAPI.Controllers
{
    [Route("api/cars")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar([FromRoute] int id)
        {
            var isDeleted = _carService.Delete(id);

            if(isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateCar([FromBody] CreateCarDto createCarDto)
        {
            var carId = _carService.Create(createCarDto);

            return Created($"api/cars/{carId}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> GetAll()
        {
            var cars = _carService.GetAll();

            return Ok(cars);
        }

        [HttpGet("{id}")]
        public ActionResult<Car> Get([FromRoute] int id)
        {
            var car = _carService.GetById(id);

            if(car is null)
            {
                return NotFound();
            }

            return Ok(car);
        }
    }
}
