using ServiceOrderApplication.Models;

namespace ServiceOrderApplication.Services;

public interface IClientService
{
    Task<List<ClientModel>> GetAllClients();
    Task<ClientModel> GetClientById(Guid id);
    Task UpdateClient(ClientModel client);
    Task<ClientModel> CreateClient(ClientModel client);
    Task DeleteClient(Guid id);
}