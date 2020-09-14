using App.Application.Domain.Models;
using App.Infra.Models.Entities;
using AutoMapper;

namespace App.Infra.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoResponseModel>()
                .ForMember(dest => dest.DataCriacao, act => act.MapFrom(src => src.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss")));
        }
    }
}