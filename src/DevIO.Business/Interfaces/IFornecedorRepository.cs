using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid id);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);

        // Convenção do DDD - Um repositório por agregação
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
