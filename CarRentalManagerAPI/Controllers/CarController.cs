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

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateCarDto updateCarDto)
        {
            var isUpdated = _carService.Update(id, updateCarDto);

            if(isUpdated)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _carService.Delete(id);

            if(isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateCarDto createCarDto)
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
