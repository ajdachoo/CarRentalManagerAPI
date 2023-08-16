using CarRentalManagerAPI.Models.Client;
using CarRentalManagerAPI.Models.Rental;
using CarRentalManagerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CarRentalManagerAPI.Controllers
{
    [Route("api/rentals")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RentalDto>> GetAll()
        {
            var rentals = _rentalService.GetAll();

            return Ok(rentals);
        }

        [HttpGet("{id}")]
        public ActionResult<RentalDto> Get([FromRoute] int id)
        {
            var rental = _rentalService.GetById(id);

            return Ok(rental);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateRentalDto createRentalDto)
        {
            var rentalId = _rentalService.Create(createRentalDto);

            return Created($"api/rentals/{rentalId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _rentalService.Delete(id);

            return NoContent();
        }

        [HttpPost("{id}/finish")]
        public ActionResult<double> Finish([FromRoute] int id, [FromBody] FinishRentalDto finishRentalDto)
        {
            double amount = _rentalService.Finish(id, finishRentalDto);

            return Ok(amount);
        }
    }
}
