﻿using DevIO.Business.Interfaces;
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

        public virtual void Adicionar(TEntiry entity)
        {
            DbSet.AddAsync(entity);
            //Removido para implementação do Unit Of Work
            //await SaveChangesAsync();
        }

        public virtual void Atualizar(TEntiry entity)
        {
            DbSet.Update(entity);
            //Removido para implementação do Unit Of Work
            //await SaveChangesAsync();
        }

        public virtual void Remover(Guid id)
        {
            DbSet.Remove(new TEntiry { Id = id });
            //Removido para implementação do Unit Of Work
            //await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            // Pode ser usando em casos excepcionais - preferencia Unit Of Work
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
