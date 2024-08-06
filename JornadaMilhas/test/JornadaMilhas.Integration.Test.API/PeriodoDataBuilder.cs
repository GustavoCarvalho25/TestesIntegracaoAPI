using Bogus;
using JornadaMilhas.Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Integration.Test.API
{
    public class PeriodoDataBuilder : Faker<Periodo>
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public PeriodoDataBuilder()
        {
            CustomInstantiator(f =>
            {
                DateTime dataInicio = DataInicio ?? f.Date.Soon();
                DateTime dataFim = DataFim ?? dataInicio.AddDays(f.Random.Int(1, 10));
                return new Periodo(dataInicio, dataFim);
            });
        }

        public Periodo Build() => Generate();
    }
}
