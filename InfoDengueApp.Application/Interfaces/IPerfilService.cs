using InfoDengueApp.Application.DTOs.Request;
using InfoDengueApp.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoDengueApp.Application.Interfaces
{
    public interface IPerfilService
    {
        Task<PerfilResponse> ObterPorIdAsync(Guid id);
        Task<IEnumerable<PerfilResponse>> ListarAsync();
        Task<PerfilResponse> CriarAsync(PerfilRequest request);
        Task AtualizarAsync(Guid id, PerfilRequest request);
        Task ExcluirAsync(Guid id);
    }
}
