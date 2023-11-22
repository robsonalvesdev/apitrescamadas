using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevIO.Data.Repository
{
    public abstract class Repository<TEntiry> : IRepository<TEntiry> where TEntiry : Entity, new()
    {
        protected readonly MeuDbContext Db;
        protected readonly DbSet<TEntiry> DbSet;

        protected Repository(MeuDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntiry>();
        }

        public virtual async Task<TEntiry?> ObterPorIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntiry>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntiry>> BuscarAsync(Expression<Func<TEntiry, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task AdicionarAsync(TEntiry entity)
        {
            await DbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public virtual async Task AtualizarAsync(TEntiry entity)
        {
            DbSet.Update(entity);
            await SaveChangesAsync();
        }

        public virtual async Task RemoverAsync(Guid id)
        {
            DbSet.Remove(new TEntiry { Id = id });
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
