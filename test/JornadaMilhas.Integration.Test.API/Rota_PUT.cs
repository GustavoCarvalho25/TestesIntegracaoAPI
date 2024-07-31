using JornadaMilhas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Integration.Test.API
{
    public class Rota_PUT : IClassFixture<JornadaMilhasWebApplicationFactory>
    {
        private readonly JornadaMilhasWebApplicationFactory _app;

        public Rota_PUT(JornadaMilhasWebApplicationFactory app)
        {
            _app = app;
        }

        [Fact]
        public async Task Atualiza_Rota()
        {
            //arrange
            var rotaExistente = _app.Context.Rota.FirstOrDefault();

            if (rotaExistente is null)
            {
                rotaExistente = new Rota()
                {
                    Origem = "Origem",
                    Destino = "Destino"
                };
                _app.Context.Add(rotaExistente);
                _app.Context.SaveChanges();
            }

            using var client = await _app.GetClientWithAccessTokenAsync();
            var rota = new
            {
                Id = rotaExistente.Id,
                Origem = "Origem",
                Destino = "Destino"
            };

            //act
            var response = await client.PutAsJsonAsync($"/rota-viagem/{rotaExistente.Id}", rota);

            //assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
