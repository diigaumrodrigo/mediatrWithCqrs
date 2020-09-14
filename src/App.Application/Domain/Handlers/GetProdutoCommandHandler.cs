using System;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Domain.Models;
using App.Application.Domain.Notifications;
using App.Application.Domain.Queries;
using App.Application.Domain.Repositories;
using MediatR;

namespace App.Application.Domain.Handlers
{
    public class GetProdutoCommandHandler : IRequestHandler<GetProdutosQueries, Response>
    {
        private readonly IMediator _mediator;
        private readonly IProdutoRepository _produtoRepository;

        public GetProdutoCommandHandler(IMediator mediator, IProdutoRepository produtoRepository)
        {
            _mediator = mediator;
            _produtoRepository = produtoRepository;
        }

        public async Task<Response> Handle(GetProdutosQueries request, CancellationToken cancellationToken)
        {
            try
            {
                var produtos = await _produtoRepository.GetProdutos();

                return new Response(produtos);
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErroNotification{ Excecao = ex.Message, PilhaErro = ex.StackTrace });
                return new Response().AddError("Problema ao listar produtos");
            }
        }
    }
}