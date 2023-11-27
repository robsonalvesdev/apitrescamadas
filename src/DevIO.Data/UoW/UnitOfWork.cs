using DevIO.Business.Interfaces;
using DevIO.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MeuDbContext _context;

        public UnitOfWork(MeuDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
