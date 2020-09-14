using System.Net;
using System.Threading.Tasks;
using App.Application.Domain.Commands;
using App.Application.Domain.Queries;
using App.WebApi.Controllers.Base;
using App.WebApi.Models.Request;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : BaseController
    {
        public ProdutosController(IMapper mapper, IMediator mediator)
            : base(mapper, mediator)
        { }

        /// <summary>
        /// Endpoint para cadastro de produto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> CadastrarProduto([FromBody] CadastrarProdutoRequestModel request)
        {
            if (request == null)
                return ErrorResponse(HttpStatusCode.BadRequest, "Solicitação inválida");

            var response = await _mediator.Send(_mapper.Map<CadastraProdutoCommand>(request));

            if (!response.Success)
                return ErrorResponse(HttpStatusCode.BadRequest, response.Errors);

            return SuccessResponse(HttpStatusCode.Created, response);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProdutos()
        {
            var response = await _mediator.Send(new GetProdutosQueries() { });

            if (!response.Success)
                return ErrorResponse(HttpStatusCode.BadRequest, response.Errors);

            return SuccessResponse(HttpStatusCode.OK, response);
        }
    }
}