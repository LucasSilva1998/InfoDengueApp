using InfoDengueApp.Domain.Entities;
using InfoDengueApp.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.Interfaces.Repository
{
    public interface IPerfilRepository : IBaseRepository<Perfil>
    {
        Task<Perfil> ObterPorIdAsync(Guid id);
        Task<Perfil> ObterPorNomeAsync(string nome);
        Task<IEnumerable<Perfil>> ObterTodosAsync();
        Task AdicionarAsync(Perfil perfil);
        Task AtualizarAsync(Perfil perfil);
        Task RemoverAsync(Guid id);
    }
}