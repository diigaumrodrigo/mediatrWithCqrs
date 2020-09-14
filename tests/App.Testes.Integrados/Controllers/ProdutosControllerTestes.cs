using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using App.Application.Domain.Models;
using App.WebApi.Models.Request;
using Newtonsoft.Json;
using Xunit;
using App.Testes.Integrados.Models;
using System;
using System.Collections.Generic;
using Xunit.Priority;

namespace App.Testes.Integrados.Controllers
{
    [CollectionDefinition("EndpointTests", DisableParallelization = true)]
    public class ProdutosControllerTestes : IClassFixture<IntegrationTestFixture>
    {
        private readonly string _nomeProdutoCadastro;
        private const string BaseUrl = "/Produtos";
        private readonly IntegrationTestFixture _fixture;

        public ProdutosControllerTestes(IntegrationTestFixture integrationTestFixture)
        {
            _nomeProdutoCadastro= $"Notebook Dell Model {DateTime.Now:ddMMyyyy_HHmmss}";
            _fixture = integrationTestFixture;
        }

        [Fact, Priority(0)]
        public async void CadastrarProduto()
        {
            var client = _fixture.GetClient();
            var url = Path.Combine(BaseUrl, "cadastrar");

            var request = new CadastrarProdutoRequestModel()
            {
                Categoria = 1,
                Valor = 100,
                Descricao = _nomeProdutoCadastro,
                Complemento = "Latitude 5340"
            };

            var response = await client.PostAsync(url, new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json));

            var content = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<Response>(content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.True(resultado.Success);
        }

        [Fact, Priority(1)]
        public async void CadastrarProdutoDeveRetornarQueProdutoJaExiste()
        {
            var client = _fixture.GetClient();
            var url = Path.Combine(BaseUrl, "cadastrar");

            var request = new CadastrarProdutoRequestModel()
            {
                Categoria = 1,
                Valor = 100,
                Descricao = _nomeProdutoCadastro,
                Complemento = "Latitude 5340"
            };

            var response = await client.PostAsync(url, new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json));

            var content = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ResponseTestes>(content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(resultado.Success);
            Assert.Equal("Produto já cadastrado", resultado.Errors.FirstOrDefault());
        }

        [Fact, Priority(2)]
        public async void CadastrarProdutoDeveRetornarQueCategoriaNaoExiste()
        {
            var client = _fixture.GetClient();
            var url = Path.Combine(BaseUrl, "cadastrar");

            var request = new CadastrarProdutoRequestModel()
            {
                Categoria = 5,
                Valor = 100,
                Descricao = "Notebook Asus",
                Complemento = "Latitude 5340"
            };

            var response = await client.PostAsync(url, new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json));

            var content = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ResponseTestes>(content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(resultado.Success);
            Assert.Equal("Categoria inválida", resultado.Errors.FirstOrDefault());
        }

        [Fact, Priority(3)]
        public async void CadastrarProdutoDeveRetornarMensagemTratadaQuandoDerExcecao()
        {
            var client = _fixture.GetClient();
            var url = Path.Combine(BaseUrl, "cadastrar");

            var request = new CadastrarProdutoRequestModel()
            {
                Categoria = 1,
                Valor = 99999999.25M,
                Descricao = "Notebook Asus",
                Complemento = "Latitude 5340"
            };

            var response = await client.PostAsync(url, new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json));

            var content = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ResponseTestes>(content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(resultado.Success);
            Assert.Equal("Problema ao cadastrar produto", resultado.Errors.FirstOrDefault());
        }

        [Fact, Priority(4)]
        public async void GetProdutosDeveRetornarProdutoCadastrado()
        {
            var client = _fixture.GetClient();

            var response = await client.GetAsync(BaseUrl);

            var content = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ResponseTestes>(content);

            var jsonProdutos = JsonConvert.SerializeObject(resultado.Result);

            IEnumerable<ProdutoResponseModel> produtos = JsonConvert
                .DeserializeObject<IEnumerable<ProdutoResponseModel>>(jsonProdutos);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(resultado.Success);
        }
    }
}