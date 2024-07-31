using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
