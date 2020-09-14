using System;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Domain.Notifications;
using MediatR;

namespace App.Application.Domain.EventHandlers
{
    public class LogEventHandler :
                                INotificationHandler<ProdutoCadastradoNotification>,
                                INotificationHandler<ErroNotification>
    {
        public Task Handle(ProdutoCadastradoNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Produto cadastrado: '{notification.Descricao}");
            });
        }

        public Task Handle(ErroNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERRO: '{notification.Excecao} \n {notification.PilhaErro}'");
            });
        }
    }
}