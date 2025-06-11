using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.Entities
{
    public class DadosEpidemiologicos
    {
        public Guid Id { get; set; }
        public string Municipio { get; set; }
        public int CodigoIbge { get; set; }
        public int SemanaEpi { get; set; }
        public string Arbovirose { get; set; }
        public int CasosEstimados { get; set; }
        public DateTime DataColeta { get; set; }
    }
}