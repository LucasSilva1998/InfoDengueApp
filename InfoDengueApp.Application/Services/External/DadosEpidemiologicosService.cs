using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using InfoDengueApp.Application.DTOs.External;
using InfoDengueApp.Application.Interfaces.ExternalServices;
using InfoDengueApp.Application.Interfaces;

namespace InfoDengueApp.Application.Services.External
{
    public class DadosEpidemiologicosService : IDadosEpidemiologicosService
    {
        private readonly IInfoDengueService _infoDengueService;

        public DadosEpidemiologicosService(IInfoDengueService infoDengueService)
        {
            _infoDengueService = infoDengueService;
        }

        public async Task<string> ObterDadosEpidemiologicosAsync(DadosEpidemiologicosFiltroRequest filtro)
        {
            if (filtro == null)
            {
                // Se filtro não for enviado, buscar dados brutos gerais
                return await _infoDengueService.ObterDadosBrutosAsync();
            }
            else
            {
                // Buscar dados filtrados conforme filtro recebido
                return await _infoDengueService.ObterFiltradoAsync(filtro);
            }
        }
    }
}