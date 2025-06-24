using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoDengueApp.Application.DTOs.Request;
using InfoDengueApp.Application.DTOs.Response;
using InfoDengueApp.Application.Interfaces;
using InfoDengueApp.Domain.Interfaces.Core;
using InfoDengueApp.Infra.Data.Extensions;
using InfoDengueApp.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InfoDengueApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<Usuario> _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(IBaseRepository<Usuario> usuarioRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<LoginResponse> AutenticarAsync(LoginRequest request)
        {
            var usuario = (await _usuarioRepository.BuscarAsync(u => u.Email == request.Email)).FirstOrDefault();

            if (usuario == null || !PasswordHasher.Verificar(request.Senha, usuario.SenhaHash))
                throw new UnauthorizedAccessException("E-mail ou senha inválidos.");

            var accessToken = GerarAccessToken(usuario);
            var refreshToken = Guid.NewGuid().ToString();
            var expiracaoRefresh = DateTime.UtcNow.AddDays(1);

            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiracao = expiracaoRefresh;
            await _usuarioRepository.AtualizarAsync(usuario);
            await _unitOfWork.CommitAsync();

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expiracao = DateTime.UtcNow.AddMinutes(5)
            };
        }

        public async Task<LoginResponse> RenovarTokenAsync(string refreshToken)
        {
            var usuario = (await _usuarioRepository.BuscarAsync(u => u.RefreshToken == refreshToken)).FirstOrDefault();

            if (usuario == null || usuario.RefreshTokenExpiracao < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token inválido ou expirado.");

            var novoAccessToken = GerarAccessToken(usuario);
            var novoRefreshToken = Guid.NewGuid().ToString();
            var expiracaoRefresh = DateTime.UtcNow.AddDays(1);

            usuario.RefreshToken = novoRefreshToken;
            usuario.RefreshTokenExpiracao = expiracaoRefresh;
            await _usuarioRepository.AtualizarAsync(usuario);
            await _unitOfWork.CommitAsync();

            return new LoginResponse
            {
                AccessToken = novoAccessToken,
                RefreshToken = novoRefreshToken,
                Expiracao = DateTime.UtcNow.AddMinutes(5)
            };
        }

        private string GerarAccessToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Role, usuario.Perfil?.Nome ?? "UsuarioComum")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

