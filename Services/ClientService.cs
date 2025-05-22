using Microsoft.EntityFrameworkCore;
using ServiceOrderApplication.Data;
using ServiceOrderApplication.Models;
using Serilog;

namespace ServiceOrderApplication.Services;

public class ClientService : IClientService
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

    public async Task<ClientModel> GetClientById(Guid id)
    {
        var client = await _context.Clients.FindAsync(id);
        if(client == null)
        {
            Log.Warning($"Client with id {id} not found");
        }
        return client;
    }
    
    public async Task<ClientModel> CreateClient(ClientModel client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        Log.Information($"Client {client.Name} created successfully");
        return client;
    }

    public Task UpdateClient(ClientModel client)
    {
        _context.Clients.Update(client);
        return _context.SaveChangesAsync();
    }
    
    public Task DeleteClient(Guid id)
    {
        var client = _context.Clients.Find(id);
        if (client == null)
        {
            Log.Warning($"Client with id {id} not found");
            return Task.CompletedTask;
        }
        _context.Clients.Remove(client);
        return _context.SaveChangesAsync();
    }
}
