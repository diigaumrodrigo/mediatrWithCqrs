using System;
using System.Net.Http;
using App.WebApi;

namespace App.Testes.Integrados
{
    public class IntegrationTestFixture : CustomWebApplicationFactory<Startup>, IDisposable
    {
        private HttpClient _cliente;

        public IntegrationTestFixture()
        {
            CreateClientApi();
        }

        private void CreateClientApi()
        {
            _cliente = CreateClient();
        }

        public HttpClient GetClient()
        {
            return _cliente;
        }
    }
}