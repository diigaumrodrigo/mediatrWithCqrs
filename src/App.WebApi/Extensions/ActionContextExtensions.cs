using System.Linq;
using App.Application.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Extensions
{
    public static class ActionContextExtensions
    {
        public static BadRequestObjectResult GetErrorsModelState(this ActionContext actionContext)
        {
            var response = new Response();
            actionContext.ModelState.Values.Where(v => v.Errors.Count > 0)
                                .SelectMany(v => v.Errors)
                                .ToList()
                                .ForEach(v => response.AddError(v.ErrorMessage));

            return new BadRequestObjectResult(response);
        }
    }
}