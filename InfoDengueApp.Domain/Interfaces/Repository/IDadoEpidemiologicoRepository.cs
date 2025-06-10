using InfoDengueApp.Domain.Entities;
using InfoDengueApp.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.Interfaces.Repository
{
    public interface IDadoEpidemiologicoRepository : IBaseRepository<DadoEpidemiologico>
    {
        Task<DadoEpidemiologico> ObterPorIdAsync(Guid id);
        Task<IEnumerable<DadoEpidemiologico>> ObterTodosAsync();
        Task<IEnumerable<DadoEpidemiologico>> ObterPorIbgeAsync(int codigoIbge);
        Task<IEnumerable<DadoEpidemiologico>> ObterPorFiltroAsync(int codigoIbge, int semanaInicio, int semanaFim, string arbovirose);
        Task AdicionarAsync(DadoEpidemiologico dado);
        Task AtualizarAsync(DadoEpidemiologico dado);
        Task RemoverAsync(Guid id);
    }
}