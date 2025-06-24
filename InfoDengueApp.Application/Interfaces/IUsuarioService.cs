using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoDengueApp.Application.DTOs.Request;
using InfoDengueApp.Application.DTOs.Response;

namespace InfoDengueApp.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> ObterPorIdAsync(Guid id);
        Task<IEnumerable<UsuarioResponse>> ListarAsync();
        Task<UsuarioResponse> CriarAsync(UsuarioRequest request);
        Task AtualizarAsync(Guid id, UsuarioRequest request);
        Task ExcluirAsync(Guid id);
    }
}
