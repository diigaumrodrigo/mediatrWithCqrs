namespace App.WebApi.Models.Request
{
    public class CadastrarProdutoRequestModel
    {
        public int Categoria { get; set; }
        public string Descricao { get; set; }
        public string Complemento { get; set; }
        public decimal Valor { get; set; }
    }
}