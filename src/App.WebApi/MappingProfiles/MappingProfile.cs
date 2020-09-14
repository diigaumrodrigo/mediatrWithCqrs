using App.Application.Domain.Commands;
using App.WebApi.Models.Request;
using AutoMapper;

namespace App.WebApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CadastrarProdutoRequestModel, CadastraProdutoCommand>();
        }
    }
}