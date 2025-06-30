using InfoDengueApp.Domain.Entities;
using InfoDengueApp.Domain.Interfaces.Repository;
using InfoDengueApp.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoDengueApp.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DataContext context) : base(context)
        {
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _dbSet.AddAsync(usuario);
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _dbSet.Update(usuario);
            await Task.CompletedTask;
        }

        public async Task RemoverAsync(Guid id)
        {
            var usuario = await ObterPorIdAsync(id);
            if (usuario != null)
                _dbSet.Remove(usuario);
        }

        public async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            return await _dbSet
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _dbSet
                .Include(u => u.Perfil) 
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Usuario>> ListarTodosAsync()
        {
            return await _dbSet
                .Include(u => u.Perfil)
                .ToListAsync();
        }


    }
}
