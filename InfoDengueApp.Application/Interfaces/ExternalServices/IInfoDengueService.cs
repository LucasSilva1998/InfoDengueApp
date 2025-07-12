using System.Threading.Tasks;
using InfoDengueApp.Application.DTOs.External;

namespace InfoDengueApp.Application.Interfaces.ExternalServices
{
    public interface IInfoDengueService
    {
        Task<string> ObterDadosBrutosAsync();
        Task<string> ObterFiltradoAsync(DadosEpidemiologicosFiltroRequest filtro);
    }
}