using InfoDengueApp.Domain.Entities;
using InfoDengueApp.Domain.Interfaces.Repository;
using InfoDengueApp.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoDengueApp.Infra.Data.Repositories
{
    public class DadoEpidemiologicoRepository : BaseRepository<DadosEpidemiologicos>, IDadoEpidemiologicoRepository
    {
        public DadoEpidemiologicoRepository(DataContext context) : base(context)
        {
        }

        public async Task<DadosEpidemiologicos> ObterPorIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<DadosEpidemiologicos>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<DadosEpidemiologicos>> ObterPorIbgeAsync(int codigoIbge)
        {
            return await _dbSet
                .Where(d => d.CodigoIbge == codigoIbge)
                .ToListAsync();
        }

        public async Task<IEnumerable<DadosEpidemiologicos>> ObterPorFiltroAsync(int codigoIbge, int semanaInicio, int semanaFim, string arbovirose)
        {
            return await _dbSet
                .Where(d =>
                    d.CodigoIbge == codigoIbge &&
                    d.SemanaEpi >= semanaInicio &&
                    d.SemanaEpi <= semanaFim &&
                    d.Arbovirose == arbovirose)
                .ToListAsync();
        }

        public async Task AdicionarAsync(DadosEpidemiologicos dado)
        {
            await _dbSet.AddAsync(dado);
        }

        public async Task AtualizarAsync(DadosEpidemiologicos dado)
        {
            _dbSet.Update(dado);
            await Task.CompletedTask;
        }

        public async Task RemoverAsync(Guid id)
        {
            var dado = await ObterPorIdAsync(id);
            if (dado != null)
            {
                _dbSet.Remove(dado);
            }
        }
    }
}
