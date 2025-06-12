using InfoDengueApp.Domain.Entities;
using InfoDengueApp.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.Interfaces.Repository
{
    public interface ILogInclusaoEpidemiologicaRepository : IBaseRepository<LogInclusaoEpidemiologica>
    {
        Task<IEnumerable<LogInclusaoEpidemiologica>> ObterPorDataAsync(DateTime data);
    }
}