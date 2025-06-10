using FluentValidation;
using InfoDengueApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.Validation
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O email é obrigatório")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(u => u.SenhaHash)
                .NotEmpty().WithMessage("Senha é obrigatória");

            RuleFor(u => u.Cpf)
                .NotNull().WithMessage("CPF é obrigatório");
            // Value Object Cpf já valida

        }
    }
}
