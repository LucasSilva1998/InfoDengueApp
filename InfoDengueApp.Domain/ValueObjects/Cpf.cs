using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.ValueObjects
{
    namespace InfoDengueApp.Domain.ValueObjects
    {
        public class Cpf
        {
            public string Numero { get; private set; }

            protected Cpf() { } // Para EF

            public Cpf(string valor)
            {
                if (string.IsNullOrWhiteSpace(valor))
                    throw new ArgumentException("CPF não pode ser vazio.");

                valor = Regex.Replace(valor, "[^0-9]", ""); // Remove pontos e traços

                if (!EhValido(valor))
                    throw new ArgumentException("CPF inválido.");

                Numero = valor;
            }

            public override string ToString()
            {
                return Convert.ToUInt64(Numero).ToString(@"000\.000\.000\-00");
            }

            private bool EhValido(string cpf)
            {
                if (cpf.Length != 11)
                    return false;

                if (new string(cpf[0], 11) == cpf)
                    return false;

                int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                string tempCpf = cpf.Substring(0, 9);
                int soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

                int resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                string digito = resto.ToString();
                tempCpf += digito;

                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito += resto.ToString();
                return cpf.EndsWith(digito);
            }

            public override bool Equals(object obj)
            {
                return obj is Cpf other && Numero == other.Numero;
            }

            public override int GetHashCode()
            {
                return Numero.GetHashCode();
            }
        }
    }
}