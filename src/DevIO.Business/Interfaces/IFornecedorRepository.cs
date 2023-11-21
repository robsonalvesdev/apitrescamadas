using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEnderecoAsync(Guid id);
        Task<Fornecedor> ObterFornecedorProdutosEnderecoAsync(Guid id);

        // Convenção do DDD - Um repositório por agregação
        Task<Endereco> ObterEnderecoPorFornecedorAsync(Guid fornecedorId);
        Task RemoverEnderecoFornecedorAsync(Endereco endereco);
    }
}
