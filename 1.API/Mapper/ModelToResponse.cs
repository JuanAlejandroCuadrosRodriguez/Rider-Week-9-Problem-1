using _1.API.Response;
using _3.Data.Models;
using AutoMapper;

namespace _1.API.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    { 
        CreateMap<Client, ClientResponse>();
    }

}