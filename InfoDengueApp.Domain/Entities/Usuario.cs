using InfoDengueApp.Domain.ValueObjects.InfoDengueApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Cpf Cpf { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }

        public Guid PerfilId { get; set; }
        public Perfil Perfil { get; set; }

        public bool Ativo { get; set; }

        public ICollection<LogAcesso> LogsAcesso { get; set; }
        public ICollection<LogInclusaoEpidemiologica> LogsInclusaoEpidemiologica { get; set; }


        // Regras de negócio
        public bool PodeGerenciarUsuarios()
        {
            return Perfil != null && Perfil.Nome == "Admin";
        }
    }
}