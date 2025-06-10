using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.Entities
{
    public class Perfil
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } // Ex: Admin, UsuarioComum

        public ICollection<Usuario> Usuarios { get; set; }
    }
}