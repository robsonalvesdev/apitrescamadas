using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorForncedor(Guid fornecedorId);
        Task<IEnumerable<Produto>> ObterProdutosForncedores();
        Task<Produto> ObterProdutoForncedor(Guid id);
    }
}
