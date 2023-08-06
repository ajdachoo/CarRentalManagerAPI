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
    [ApiController]
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
            _carService.Update(id, updateCarDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _carService.Delete(id);

            return NoContent();
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
        public ActionResult<CarDto> Get([FromRoute] int id)
        {
            var car = _carService.GetById(id);

            return Ok(car);
        }
    }
}
