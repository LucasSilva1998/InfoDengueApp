using InfoDengueApp.Domain.Entities;
using InfoDengueApp.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.Interfaces.Repository
{
    public interface IDadoEpidemiologicoRepository : IBaseRepository<DadosEpidemiologicos>
    {
        Task<DadosEpidemiologicos> ObterPorIdAsync(Guid id);
        Task<IEnumerable<DadosEpidemiologicos>> ObterTodosAsync();
        Task<IEnumerable<DadosEpidemiologicos>> ObterPorIbgeAsync(int codigoIbge);
        Task<IEnumerable<DadosEpidemiologicos>> ObterPorFiltroAsync(int codigoIbge, int semanaInicio, int semanaFim, string arbovirose);
        Task AdicionarAsync(DadosEpidemiologicos dado);
        Task AtualizarAsync(DadosEpidemiologicos dado);
        Task RemoverAsync(Guid id);
    }
}