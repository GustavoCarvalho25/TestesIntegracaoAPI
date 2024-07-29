using JornadaMilhas.API.DTO.Auth;
using System.Net.Http.Json;
using System.Net;

namespace JornadaMilhas.Integration.Test.API
{
    public class JornadaMilhas_AuthTest
    {
        [Fact]
        public async Task POST_Efetua_Login_Com_Sucesso()
        {

            //Arrange
            var app = new JornadaMilhasWebApplicationFactory();

            var user = new UserDTO { Email = "tester@email.com", Password = "Senha123@" };
            using var client = app.CreateClient();

            //Act
            var result = await client.PostAsJsonAsync("/auth-login", user);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task POST_Efetua_Login_Com_Falha()
        {
            //arrange
            var app = new JornadaMilhasWebApplicationFactory();

            var user = new UserDTO { Email = "errado@email.com", Password = "SenhaErrada" };
            using var client = app.CreateClient();

            //act
            var result = await client.PostAsJsonAsync("/auth-login", user);

            //assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}