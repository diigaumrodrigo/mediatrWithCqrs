using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Domain.Commands;
using App.Application.Domain.Models;
using App.Application.Domain.Queries;
using App.WebApi.Controllers;
using App.WebApi.Models.Request;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace App.Testes.Unidade.Controllers.Produto
{
    public class ProdutosControllerTestes
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IMediator> _mediatorMock;
        private ProdutosController _produtoController;

        public ProdutosControllerTestes()
        {
            _mapperMock = new Mock<IMapper>();
            _mediatorMock = new Mock<IMediator>();

            _produtoController = new ProdutosController(
                _mapperMock.Object,
                _mediatorMock.Object
            );
        }

        [Fact]
        public async Task CadastrarProdutoDeveRetornoCreatedCasoOProdutoSejaCadastrado()
        {
            var request = new CadastrarProdutoRequestModel()
            {
                Categoria = 1,
                Valor = 100,
                Descricao = "Notebook"
            };

            var responseEsperado = new Response();

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<CadastraProdutoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseEsperado);

            var resultado = (await _produtoController.CadastrarProduto(request)) as ObjectResult;
            var dados = resultado.Value as Response;

            _mediatorMock
                .Verify(x => x.Send(It.IsAny<CadastraProdutoCommand>(), It.IsAny<CancellationToken>()), Times.Once());

            Assert.Equal((int)HttpStatusCode.Created, (int)resultado.StatusCode);
            Assert.True(dados.Success);
            Assert.Equal(0, (int)dados.Errors.Count());
        }

        [Fact]
        public async Task CadastrarProdutoDeveRetornoBadRequestCasoRequestSejaNull()
        {
            var resultado = (await _produtoController.CadastrarProduto(null)) as ObjectResult;
            var dados = resultado.Value as Response;

            Assert.Equal((int)HttpStatusCode.BadRequest, (int)resultado.StatusCode);
            Assert.False(dados.Success);
            Assert.Equal("Solicitação inválida", dados.Errors.FirstOrDefault());
        }

        [Fact]
        public async Task CadastrarProdutoDeveRetornoBadRequestCasoDescricaoSejaNull()
        {
            var request = new CadastrarProdutoRequestModel()
            {
                Categoria = 1,
                Valor = 100,
                Descricao = null
            };

            var responseEsperado = new Response()
                .AddError("Descrição do produto é obrigatória");

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<CadastraProdutoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseEsperado)
                .Verifiable("Response não enviado");

            var resultado = (await _produtoController.CadastrarProduto(request)) as ObjectResult;
            var dados = resultado.Value as Response;

            _mediatorMock
                .Verify(x => x.Send(It.IsAny<CadastraProdutoCommand>(), It.IsAny<CancellationToken>()), Times.Once());

            Assert.Equal((int)HttpStatusCode.BadRequest, (int)resultado.StatusCode);
            Assert.False(dados.Success);
            Assert.Equal(responseEsperado.Errors.First(), dados.Errors.FirstOrDefault());
        }

        [Fact]
        public async Task CadastrarProdutoDeveRetornoBadRequestCasoCategoriaSejaInvalida()
        {
            var request = new CadastrarProdutoRequestModel()
            {
                Categoria = 0,
                Valor = 100,
                Descricao = "Notebook"
            };

            var responseEsperado = new Response()
                .AddError("Categoria é obrigatória");

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<CadastraProdutoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseEsperado)
                .Verifiable("Response não enviado");

            var resultado = (await _produtoController.CadastrarProduto(request)) as ObjectResult;
            var dados = resultado.Value as Response;

            _mediatorMock
                .Verify(x => x.Send(It.IsAny<CadastraProdutoCommand>(), It.IsAny<CancellationToken>()), Times.Once());

            Assert.Equal((int)HttpStatusCode.BadRequest, (int)resultado.StatusCode);
            Assert.False(dados.Success);
            Assert.Equal(responseEsperado.Errors.First(), dados.Errors.FirstOrDefault());
        }

        [Fact]
        public async Task CadastrarProdutoDeveRetornoBadRequestCasoValorSejaZero()
        {
            var request = new CadastrarProdutoRequestModel()
            {
                Categoria = 1,
                Valor = 0,
                Descricao = "Notebook"
            };

            var responseEsperado = new Response()
                .AddError("Valor do produto deve ser maior que zero");

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<CadastraProdutoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseEsperado)
                .Verifiable("Response não enviado");

            var resultado = (await _produtoController.CadastrarProduto(request)) as ObjectResult;
            var dados = resultado.Value as Response;

            _mediatorMock
                .Verify(x => x.Send(It.IsAny<CadastraProdutoCommand>(), It.IsAny<CancellationToken>()), Times.Once());

            Assert.Equal((int)HttpStatusCode.BadRequest, (int)resultado.StatusCode);
            Assert.False(dados.Success);
            Assert.Equal(responseEsperado.Errors.First(), dados.Errors.FirstOrDefault());
        }

        [Fact]
        public async Task GetProdutoDeveRetornarOk()
        {
            var responseEsperado = new Response();

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetProdutosQueries>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseEsperado);

            var resultado = (await _produtoController.GetProdutos()) as ObjectResult;
            var dados = resultado.Value as Response;

            _mediatorMock
                .Verify(x => x.Send(It.IsAny<GetProdutosQueries>(), It.IsAny<CancellationToken>()), Times.Once());

            Assert.Equal((int)HttpStatusCode.OK, (int)resultado.StatusCode);
            Assert.True(dados.Success);
            Assert.Equal(0, (int)dados.Errors.Count());
        }

        [Fact]
        public async Task GetProdutoDeveRetornarBadRequestOcorraAlgumProblemaNaConsulta()
        {
            var responseEsperado = new Response().AddError("Problema ao listar produtos");

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetProdutosQueries>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseEsperado);

            var resultado = (await _produtoController.GetProdutos()) as ObjectResult;
            var dados = resultado.Value as Response;

            _mediatorMock
                .Verify(x => x.Send(It.IsAny<GetProdutosQueries>(), It.IsAny<CancellationToken>()), Times.Once());

            Assert.Equal((int)HttpStatusCode.BadRequest, (int)resultado.StatusCode);
            Assert.False(dados.Success);
            Assert.Equal(responseEsperado.Errors.First(), dados.Errors.FirstOrDefault());
        }
    }
}