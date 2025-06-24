using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoDengueApp.Application.DTOs.Request;
using InfoDengueApp.Application.DTOs.Response;

namespace InfoDengueApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> AutenticarAsync(LoginRequest request);
        Task<LoginResponse> RenovarTokenAsync(string refreshToken);
    }
}
