using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository, INotificador notificador, IUnitOfWork unitOfWork) : base(notificador, unitOfWork)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task AdicionarAsync(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao<FornecedorValidation, Fornecedor>(new(), fornecedor) || !ExecutarValidacao<EnderecoValidation, Endereco>(new(), fornecedor.Endereco!)) return;

            if (_fornecedorRepository.BuscarAsync(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Ja existe um fornecedor com este documento informado");
                return;
            }

            _fornecedorRepository.Adicionar(fornecedor);

            await CommitAsync(); //UoW
        }

        public async Task AtualizarAsync(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao<FornecedorValidation, Fornecedor>(new(), fornecedor)) return;

            if (_fornecedorRepository.BuscarAsync(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
            {
                Notificar("Ja existe um fornecedor com este documento informado");
                return;
            }

            _fornecedorRepository.Atualizar(fornecedor);

            await CommitAsync(); //UoW
        }

        public async Task RemoverAsync(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorProdutosEnderecoAsync(id);

            if (fornecedor == null) 
            {
                Notificar("Fornecedor não existe");
                return;
            }

            if (fornecedor.Produtos!.Any())
            {
                Notificar("O fornecedor possue produtos cadastrados");
                return;
            }

            var endereco = await _fornecedorRepository.ObterEnderecoPorFornecedorAsync(id);

            if (endereco != null)
            {
                _fornecedorRepository.RemoverEnderecoFornecedorAsync(endereco);
            }

            _fornecedorRepository.Remover(id);

            await CommitAsync(); //UoW
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
        }

    }
}
