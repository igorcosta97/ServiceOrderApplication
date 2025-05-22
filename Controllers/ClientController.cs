using Microsoft.AspNetCore.Mvc;
using ServiceOrderApplication.Services;

namespace ServiceOrderApplication.Controllers;

[ApiController]
[Route("client")]  
public class ClientController : ControllerBase
{
    // GET
    private readonly ClientService _clientService;
    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _clientService.GetAllClients();
        return Ok(clients);
    }
}