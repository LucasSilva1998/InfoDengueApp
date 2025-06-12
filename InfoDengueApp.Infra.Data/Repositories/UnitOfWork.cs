using InfoDengueApp.Domain.Interfaces.Core;
using InfoDengueApp.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InfoDengueApp.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public Task RollbackAsync()
        {
            // Como não estamos usando transações explícitas ainda, apenas um stub.
            // Podemos melhorar isso quando implementarmos transações reais (ex: com TransactionScope ou BeginTransaction).
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
