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
    public class OfertaViagem_GET : IClassFixture<JornadaMilhasWebApplicationFactory>
    {
        private readonly JornadaMilhasWebApplicationFactory _app;

        public OfertaViagem_GET(JornadaMilhasWebApplicationFactory app)
        {
            _app = app;
        }

        [Fact]
        public async Task Recupera_Viagem_Por_Id()
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

            //act
            var response = await client.GetFromJsonAsync<OfertaViagem>($"/ofertas-viagem/{ofertaExistente.Id}");

            //assert
            Assert.NotNull(response);
            Assert.Equal(ofertaExistente.Id, response.Id);
            Assert.Equal(ofertaExistente.Preco, response.Preco, 0.001);
            Assert.Equal(ofertaExistente.Rota.Origem, response.Rota.Origem);
            Assert.Equal(ofertaExistente.Rota.Destino, response.Rota.Destino);
        }
    }
}
