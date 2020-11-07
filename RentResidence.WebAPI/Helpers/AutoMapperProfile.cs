using AutoMapper;
using RentResidence.Domain;
using RentResidence.WebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentResidence.WebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Residence, ResidenceDto>().ReverseMap();
        }
    }
}






