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
    public class LogInclusaoEpidemiologicaRepository : BaseRepository<LogInclusaoEpidemiologica>, ILogInclusaoEpidemiologicaRepository
    {
        public LogInclusaoEpidemiologicaRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<LogInclusaoEpidemiologica>> ObterPorDataAsync(DateTime data)
        {
            return await _dbSet
                .Where(l => l.DataHoraInclusao.Date == data.Date)
                .OrderByDescending(l => l.DataHoraInclusao)
                .ToListAsync();
        }
    }
}
