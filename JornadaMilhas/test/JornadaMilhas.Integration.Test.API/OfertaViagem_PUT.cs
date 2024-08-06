using JornadaMilhas.Dominio.Entidades;
using JornadaMilhas.Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Integration.Test.API
{
    public class OfertaViagem_PUT : IClassFixture<JornadaMilhasWebApplicationFactory>
    {
        private readonly JornadaMilhasWebApplicationFactory _app;

        public OfertaViagem_PUT(JornadaMilhasWebApplicationFactory app)
        {
            _app = app;
        }


        [Fact]
        public async Task Atualiza_OfertaViagem()
        {
            //arrange
            var ofertaExistente = _app.Context.OfertasViagem.FirstOrDefault();

            if (ofertaExistente is null)
            {
                ofertaExistente = new OfertaViagem()
                {
                    Preco = 100,
                    Rota = new Rota("Origem", "Destino"),
                    Periodo = new Periodo(DateTime.Parse("2024-03-03"), DateTime.Parse("2024-03-06"))
                };
                _app.Context.Add(ofertaExistente);
                _app.Context.SaveChanges();
            }

            using var client = await _app.GetClientWithAccessTokenAsync();
            ofertaExistente.Rota.Origem = "Origem Alterada";
            ofertaExistente.Rota.Destino = "Destino Alterado";

            //act
            var response = await client.PutAsJsonAsync($"/ofertas-viagem/",ofertaExistente);

            //assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
