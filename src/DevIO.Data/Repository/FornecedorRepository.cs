using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MeuDbContext context) : base(context)
        {            
        }

        public async Task<Fornecedor?> ObterFornecedorEnderecoAsync(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Fornecedor?> ObterFornecedorProdutosEnderecoAsync(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include (c => c.Produtos)
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Endereco?> ObterEnderecoPorFornecedorAsync(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }

        public void RemoverEnderecoFornecedorAsync(Endereco endereco)
        {
            Db.Enderecos.Remove(endereco);
            //Removido para implementação do Unit Of Work
            //await SaveChangesAsync();
        }
    }
}
