using MediatR;

namespace App.Application.Domain.Notifications
{
    public class ErroNotification: INotification
    {
        public string Excecao { get; set; }
        public string PilhaErro { get; set; }
    }
}