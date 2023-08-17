using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Models.Car;
using CarRentalManagerAPI.Models.Client;
using CarRentalManagerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace CarRentalManagerAPI.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientDto>> GetAll([FromQuery] bool? isBlocked)
        {
            var clients = _clientService.GetAll(isBlocked);

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public ActionResult<ClientDto> Get([FromRoute] int id)
        {
            var client = _clientService.GetById(id);

            return Ok(client);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateClientDto createClientDto)
        {
            var clientId = _clientService.Create(createClientDto);

            return Created($"api/clients/{clientId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _clientService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateClientDto updateClientDto)
        {
            _clientService.Update(id, updateClientDto);

            return Ok();
        }
    }
}
