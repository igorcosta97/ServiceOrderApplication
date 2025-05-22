using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceOrderApplication.Models;
using ServiceOrderApplication.Services;

namespace ServiceOrderApplication.Controllers;

[ApiController]
[Route("client")]  
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public ClientController(ClientService clientService, IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _clientService.GetAllClients();
        return Ok(clients);
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetClientById(Guid id)
    {
        var client = await _clientService.GetClientById(id);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] ClientDTO client)
    {
        if (client == null)
        {
            return BadRequest("Client cannot be null");
        }
        var clientModel = _mapper.Map<ClientModel>(client);
        
        var createdClient = await _clientService.CreateClient(clientModel);
        return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);
    }
    
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateClient(Guid id, [FromBody] ClientDTO client)
    {
        if (client == null)
        {
            return BadRequest("Client cannot be null");
        }
        
        var existingClient = await _clientService.GetClientById(id);
        if (existingClient == null)
        {
            return NotFound();
        }
        
        _mapper.Map(client, existingClient);
        
        await _clientService.UpdateClient(existingClient);

        return Ok(existingClient);
    }
    
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteClient(Guid id)
    {
        var existingClient = await _clientService.GetClientById(id);
        if (existingClient == null)
        {
            return NotFound();
        }
        
        await _clientService.DeleteClient(id);
        return NoContent();
    }
}