using System;
using System.Threading.Tasks;

namespace InfoDengueApp.Domain.Interfaces.Core
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Persiste as alterações feitas na unidade de trabalho.
        /// </summary>
        /// <returns>Retorna true se o commit foi bem-sucedido.</returns>
        Task<bool> CommitAsync();

        /// <summary>
        /// Opcional: pode implementar para reverter transações.
        /// </summary>
        Task RollbackAsync();
    }
}
