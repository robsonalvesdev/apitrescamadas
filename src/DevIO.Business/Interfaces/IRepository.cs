using DevIO.Business.Models;
using System.Linq.Expressions;

namespace DevIO.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> ObterPorIdAsync(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate);
        Task AdicionarAsync(TEntity entity);
        Task AtualizarAsync(TEntity entity);        
        Task RemoverAsync(Guid id);        
        Task<int> SaveChangesAsync();
    }
}
