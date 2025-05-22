using Microsoft.EntityFrameworkCore;
using ServiceOrderApplication.Data;
using ServiceOrderApplication.Models;
using Serilog;

namespace ServiceOrderApplication.Services;

public class ClientService
{
    private readonly AppDbContext _context;

    public ClientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ClientModel>> GetAllClients()
    {
        var clients = await _context.Clients.ToListAsync();

        if (!clients.Any())
        {
            Log.Warning("No clients found");
        }

        return clients;
    }
}
