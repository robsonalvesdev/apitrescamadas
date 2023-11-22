using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorForncedorAsync(Guid fornecedorId);
        Task<IEnumerable<Produto>> ObterProdutosFornecedoresAsync();
        Task<Produto> ObterProdutoFornecedorAsync(Guid id);
    }
}
