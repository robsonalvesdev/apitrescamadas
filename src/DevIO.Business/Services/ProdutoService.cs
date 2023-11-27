using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository, INotificador notificador, IUnitOfWork unitOfWork) : base(notificador, unitOfWork)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task AdicionarAsync(Produto produto)
        {
            if (!ExecutarValidacao<ProdutoValidation, Produto>(new(), produto)) return;

            var produtoExistente = await _produtoRepository.ObterPorIdAsync(produto.Id);
            if (produtoExistente != null)
            {
                Notificar("Já existe um produto com o ID informado!");
                return;
            }

            _produtoRepository.Adicionar(produto);

            await CommitAsync(); //UoW
        }

        public async Task AtualizarAsync(Produto produto)
        {
            if (!ExecutarValidacao<ProdutoValidation, Produto>(new(), produto)) return;
            _produtoRepository.Atualizar(produto);
            await CommitAsync(); //UoW
        }

        public async Task RemoverAsync(Guid id)
        {            
            var produto = await _produtoRepository.ObterProdutoFornecedorAsync(id);

            if (produto == null)
            {
                Notificar("Produto não existe");
                return;
            }

            _produtoRepository.Remover(id);

            await CommitAsync(); //UoW
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
