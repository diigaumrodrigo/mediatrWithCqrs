using MediatR;

namespace App.Application.Domain.Notifications
{
    public class ProdutoCadastradoNotification : INotification
    {
        public string Descricao { get; set; }
    }
}