using System;

namespace App.Infra.Models.Entities
{
    public class CategoriaProduto
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}