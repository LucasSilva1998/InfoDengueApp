using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using InfoDengueApp.Application.DTOs.External;

namespace InfoDengueApp.Application.Interfaces.ExternalServices
{
    public interface IDadosEpidemiologicosService
    {
        Task<string> ObterDadosEpidemiologicosAsync(DadosEpidemiologicosFiltroRequest filtro);
    }
}