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
    public class PerfilRepository : BaseRepository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(DataContext context) : base(context)
        {
        }

        public async Task AdicionarAsync(Perfil perfil)
        {
            await _dbSet.AddAsync(perfil);
        }

        public async Task AtualizarAsync(Perfil perfil)
        {
            _dbSet.Update(perfil);
            await Task.CompletedTask;
        }

        public async Task RemoverAsync(Guid id)
        {
            var perfil = await ObterPorIdAsync(id);
            if (perfil != null)
                _dbSet.Remove(perfil);
        }

        public async Task<Perfil> ObterPorIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Perfil> ObterPorNomeAsync(string nome)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Nome == nome);
        }

        public async Task<IEnumerable<Perfil>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
