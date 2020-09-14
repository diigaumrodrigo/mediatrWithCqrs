using System.Net;
using App.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace App.Testes.Unidade.Extensions
{
    public class ActionContextExtensionsTestes
    {
        [Fact]
        public void GetErrorsModelStateDeveRetornarBadRequestObjectResult()
        {
            var typeExpected = typeof(BadRequestObjectResult);
            var actionContextMock = new Mock<ActionContext>();

            actionContextMock.Object.ModelState.AddModelError("Propriedade", "Propriedade é obrigatória");

            var resultado = ActionContextExtensions.GetErrorsModelState(actionContextMock.Object);

            Assert.Equal((int)HttpStatusCode.BadRequest, (int)resultado.StatusCode);
            Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType(typeExpected, resultado);
        }
    }
}