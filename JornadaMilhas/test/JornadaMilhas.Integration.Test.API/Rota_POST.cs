using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Integration.Test.API
{
    public class Rota_POST : IClassFixture<JornadaMilhasWebApplicationFactory>
    {
        private readonly JornadaMilhasWebApplicationFactory _app;

        public Rota_POST(JornadaMilhasWebApplicationFactory app)
        {
            _app = app;
        }

        [Fact]
        public async Task Cadastra_Rota()
        {
            //arrange
            var rotaExistente = _app.Context.Rota.FirstOrDefault();

            if (rotaExistente is not null)
            {
                _app.Context.Remove(rotaExistente);
                _app.Context.SaveChanges();
            }

            using var client = await _app.GetClientWithAccessTokenAsync();
            var rota = new
            {
                Origem = "Origem",
                Destino = "Destino"
            };

            //act
            var response = await client.PostAsJsonAsync($"/rota-viagem/", rota);

            //assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
