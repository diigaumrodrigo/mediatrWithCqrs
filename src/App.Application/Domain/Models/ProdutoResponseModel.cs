using System;

namespace App.Application.Domain.Models
{
    public class ProdutoResponseModel
    {
        public Guid ProdutoId { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public string Complemento { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        public string DataCriacao { get; set; }
    }
}