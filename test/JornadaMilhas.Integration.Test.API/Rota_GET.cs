using JornadaMilhas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Integration.Test.API
{
    public class Rota_GET: IClassFixture<JornadaMilhasWebApplicationFactory>
    {
        private readonly JornadaMilhasWebApplicationFactory _app;

        public Rota_GET(JornadaMilhasWebApplicationFactory app)
        {
            _app = app;
        }

        [Fact]
        public async Task Recupera_Rota_Por_Id()
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

            //act
            var response = await client.GetFromJsonAsync<Rota>($"/rota-viagem/{rotaExistente.Id}");

            //assert
            Assert.NotNull(response);
            Assert.Equal(rotaExistente.Id, response.Id);
            Assert.Equal(rotaExistente.Origem, response.Origem);
            Assert.Equal(rotaExistente.Destino, response.Destino);
        }
    }
}
