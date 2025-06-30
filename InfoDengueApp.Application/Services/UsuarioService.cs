using InfoDengueApp.Application.DTOs.Request;
using InfoDengueApp.Application.DTOs.Response;
using InfoDengueApp.Application.Interfaces;
using InfoDengueApp.Domain.Entities;
using InfoDengueApp.Domain.Interfaces.Core;
using InfoDengueApp.Domain.Interfaces.Repository;
using InfoDengueApp.Domain.ValueObjects;
using InfoDengueApp.Domain.ValueObjects.InfoDengueApp.Domain.ValueObjects;
using InfoDengueApp.Infra.Data.Extensions;

namespace InfoDengueApp.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UsuarioResponse> CriarAsync(UsuarioRequest request)
        {
            var emailExiste = await _usuarioRepository.ExisteEmailAsync(request.Email);
            if (emailExiste)
                throw new Exception("E-mail já cadastrado.");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = PasswordHasher.Hash(request.Senha),
                PerfilId = request.PerfilId,
                Cpf = new Cpf(request.Cpf),
                Ativo = true
            };

            await _usuarioRepository.AdicionarAsync(usuario);
            await _unitOfWork.CommitAsync();

            return MapToResponse(usuario);
        }

        public async Task<UsuarioResponse> ObterPorIdAsync(Guid id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            return MapToResponse(usuario);
        }

        public async Task<IEnumerable<UsuarioResponse>> ListarAsync()
        {
            var usuarios = await _usuarioRepository.ListarTodosAsync();

            return usuarios.Select(MapToResponse);
        }

        public async Task AtualizarAsync(Guid id, UsuarioRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            usuario.Nome = request.Nome;
            usuario.Email = request.Email;
            usuario.PerfilId = request.PerfilId;
            usuario.SenhaHash = PasswordHasher.Hash(request.Senha);
            usuario.Cpf = new Cpf(request.Cpf);

            await _usuarioRepository.AtualizarAsync(usuario);
            await _unitOfWork.CommitAsync();
        }

        public async Task ExcluirAsync(Guid id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            await _usuarioRepository.RemoverAsync(id);
            await _unitOfWork.CommitAsync();
        }

        private static UsuarioResponse MapToResponse(Usuario usuario)
        {
            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil?.Nome ?? string.Empty
            };
        }
    }
}
