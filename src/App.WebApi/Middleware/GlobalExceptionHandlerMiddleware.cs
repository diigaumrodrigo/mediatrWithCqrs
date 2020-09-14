using App.Application.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace App.WebApi.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        public GlobalExceptionHandlerMiddleware()
        { }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception)
            {
                await HandleExceptionAsync(context);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new Response();
            
            response
                .AddError("Ocorreu um erro inesperado no sistema. Por favor tente novamente e caso o problema permaneça, entre em contato com o suporte.");

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}