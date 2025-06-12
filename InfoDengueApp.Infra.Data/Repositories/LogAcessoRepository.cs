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
    public class LogAcessoRepository : BaseRepository<LogAcesso>, ILogAcessoRepository
    {
        public LogAcessoRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<LogAcesso>> ObterPorUsuarioAsync(Guid usuarioId)
        {
            return await _dbSet
                .Where(l => l.UsuarioId == usuarioId)
                .OrderByDescending(l => l.DataHora)
                .ToListAsync();
        }
    }
}
