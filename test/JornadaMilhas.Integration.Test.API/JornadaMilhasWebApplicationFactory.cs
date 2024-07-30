using JornadaMilhas.API.DTO.Auth;
using JornadaMilhas.Dados;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace JornadaMilhas.Integration.Test.API
{
    public class JornadaMilhasWebApplicationFactory : WebApplicationFactory<Program>
    {
        public JornadaMilhasContext Context { get; private set; }
        private IServiceScope _scope;

        public JornadaMilhasWebApplicationFactory()
        {
            _scope = Services.CreateScope();
            Context = _scope.ServiceProvider.GetRequiredService<JornadaMilhasContext>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof
                    (DbContextOptions<JornadaMilhasContext>));
                //O ideal é usar secrets ou settings para não expor os dados de conexão
                services.AddDbContext<JornadaMilhasContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseSqlServer
                ("Server=localhost,11433;Database=JornadaMilhasV3;" +
                "User Id=sa;Password=Alura#2024;Encrypt=false;TrustServerCertificate=true;" +
                "MultipleActiveResultSets=true;"));
            });

            base.ConfigureWebHost(builder);
        }

        public async Task<HttpClient> GetClientWithAccessTokenAsync()
        {
            var client = CreateClient();
            var user = new UserDTO { Email = "tester@email.com", Password = "Senha123@" };
            var result = await client.PostAsJsonAsync("/auth-login", user);

            result.EnsureSuccessStatusCode();

            var resultContent = await result.Content.ReadFromJsonAsync<UserTokenDTO>();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultContent!.Token);

            return client;
        }
    }
}
