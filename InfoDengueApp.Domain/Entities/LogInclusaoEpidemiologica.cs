using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.Entities
{
    public class LogInclusaoEpidemiologica
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid DadoEpidemiologicoId { get; set; }
        public DateTime DataHoraInclusao { get; set; }

        public Usuario Usuario { get; set; }
        public DadoEpidemiologico DadoEpidemiologico { get; set; }
    }
}