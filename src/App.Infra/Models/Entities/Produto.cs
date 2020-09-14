using System;
using System.Collections.Generic;

namespace App.Infra.Models.Entities
{
    public class Produto
    {
        public Produto(Guid produtoId, int categoriaProdutoId, string descricao, string complemento, decimal valor, bool ativo, DateTime dataCriacao)
        {
            this.ProdutoId = produtoId;
            this.CategoriaProdutoId = categoriaProdutoId;
            this.Descricao = descricao;
            this.Complemento = complemento;
            this.Valor = valor;
            this.Ativo = ativo;
            this.DataCriacao = dataCriacao;

        }

        public Produto()
        {
            CategoriaProduto = new HashSet<CategoriaProduto>();
        }

        public Guid ProdutoId { get; set; }
        public int CategoriaProdutoId { get; set; }
        public string Descricao { get; set; }
        public string Complemento { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }

        public virtual ICollection<CategoriaProduto> CategoriaProduto { get; set; }
    }
}