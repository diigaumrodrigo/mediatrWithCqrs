using System;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Domain.Commands;
using App.Application.Domain.Models;
using App.Application.Domain.Notifications;
using App.Application.Domain.Repositories;
using MediatR;

namespace App.Application.Domain.Handlers
{
    public class CadastraProdutoCommandHandler : IRequestHandler<CadastraProdutoCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IProdutoRepository _produtoRepository;

        public CadastraProdutoCommandHandler(IMediator mediator, IProdutoRepository produtoRepository)
        {
            _mediator = mediator;
            _produtoRepository = produtoRepository;
        }

        async Task<Response> IRequestHandler<CadastraProdutoCommand, Response>.Handle(CadastraProdutoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _produtoRepository.Cadastrar(request);

                await _mediator.Publish(new ProdutoCadastradoNotification
                {
                    Descricao = request.Descricao
                });

                return new Response("Produto cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErroNotification{ Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return new Response().AddError("Problema ao cadastrar produto");
            }
        }
    }
}