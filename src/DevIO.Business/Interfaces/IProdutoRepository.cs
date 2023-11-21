using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorForncedorAsync(Guid fornecedorId);
        Task<IEnumerable<Produto>> ObterProdutosForncedoresAsync();
        Task<Produto> ObterProdutoForncedorAsync(Guid id);
    }
}
