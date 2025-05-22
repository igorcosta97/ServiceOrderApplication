using AutoMapper;
using ServiceOrderApplication.Models;

namespace ServiceOrderApplication.Profiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientDTO, ClientModel>(); 
    }
}
