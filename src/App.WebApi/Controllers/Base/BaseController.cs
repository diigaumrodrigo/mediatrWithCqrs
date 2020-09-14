using System.Collections.Generic;
using System.Linq;
using System.Net;
using App.Application.Domain.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediator;

        protected BaseController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        protected IActionResult SuccessResponse<T>(HttpStatusCode statusCode, T data)
        {
            var response = data is Response ? data as Response : new Response(data);

            return StatusCode((int)statusCode, response);
        }

        protected IActionResult ErrorResponse(HttpStatusCode statusCode, string erro)
        {
            return ErrorResponse(statusCode, new string[] { erro });
        }

        protected IActionResult ErrorResponse(HttpStatusCode statusCode, IEnumerable<string> erros)
        {
            var response = new Response();

            foreach (var erro in erros.Distinct())
                response.AddError(erro);

            return StatusCode((int)statusCode, response);
        }
    }
}