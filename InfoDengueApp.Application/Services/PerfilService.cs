using InfoDengueApp.Application.DTOs.Request;
using InfoDengueApp.Application.DTOs.Response;
using InfoDengueApp.Application.Interfaces;
using InfoDengueApp.Domain.Entities;
using InfoDengueApp.Domain.Interfaces.Repository;
using InfoDengueApp.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoDengueApp.Application.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PerfilService(IPerfilRepository perfilRepository, IUnitOfWork unitOfWork)
        {
            _perfilRepository = perfilRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PerfilResponse> ObterPorIdAsync(Guid id)
        {
            var perfil = await _perfilRepository.ObterPorIdAsync(id);
            if (perfil == null)
                throw new Exception("Perfil não encontrado.");

            return new PerfilResponse
            {
                Id = perfil.Id,
                Nome = perfil.Nome
            };
        }

        public async Task<IEnumerable<PerfilResponse>> ListarAsync()
        {
            var perfis = await _perfilRepository.ObterTodosAsync();
            return perfis.Select(p => new PerfilResponse
            {
                Id = p.Id,
                Nome = p.Nome
            });
        }

        public async Task<PerfilResponse> CriarAsync(PerfilRequest request)
        {
            var perfil = new Perfil
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome
            };

            await _perfilRepository.AdicionarAsync(perfil);
            await _unitOfWork.CommitAsync();

            return new PerfilResponse
            {
                Id = perfil.Id,
                Nome = perfil.Nome
            };
        }

        public async Task AtualizarAsync(Guid id, PerfilRequest request)
        {
            var perfil = await _perfilRepository.ObterPorIdAsync(id);
            if (perfil == null)
                throw new Exception("Perfil não encontrado.");

            perfil.Nome = request.Nome;

            await _perfilRepository.AtualizarAsync(perfil);
            await _unitOfWork.CommitAsync();
        }

        public async Task ExcluirAsync(Guid id)
        {
            await _perfilRepository.RemoverAsync(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
