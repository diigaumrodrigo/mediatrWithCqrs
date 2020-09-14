using App.Application.Domain.Models;
using MediatR;

namespace App.Application.Domain.Commands
{
    public class CadastraProdutoCommand : IRequest<Response>
    {
        public int Categoria { get; set; }
        public string Descricao { get; set; }
        public string Complemento { get; set; }
        public decimal Valor { get; set; }

        public CadastraProdutoCommand(int categoria, string descricao, string complemento, decimal valor)
        {
            Categoria = categoria;
            Descricao = descricao;
            Complemento = complemento;
            Valor = valor;
        }
    }
}