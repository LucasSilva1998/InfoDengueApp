using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.Entities
{
    public class LogAcesso
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Acao { get; set; }
        public DateTime DataHora { get; set; }
        public string Ip { get; set; }

        public Usuario Usuario { get; set; }
    }
}