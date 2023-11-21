﻿using DevIO.Business.Models;
using System.Linq.Expressions;

namespace DevIO.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task AdicionarAsync(TEntity entity);
        Task<TEntity> ObterPorIdAsync(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task AtualizarAsync(TEntity entity);
        Task RemoverAsync(Guid id);
        Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChangesAsync();
    }
}
