using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository, INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task AdicionarAsync(Produto produto)
        {
            if (!ExecutarValidacao<ProdutoValidation, Produto>(new(), produto)) return;
            await _produtoRepository.AdicionarAsync(produto);
        }

        public async Task AtualizarAsync(Produto produto)
        {
            if (!ExecutarValidacao<ProdutoValidation, Produto>(new(), produto)) return;
            await _produtoRepository.AtualizarAsync(produto);
        }

        public async Task RemoverAsync(Guid id)
        {            
            var produto = await _produtoRepository.ObterProdutoFornecedorAsync(id);

            if (produto == null)
            {
                Notificar("Produto não existe");
                return;
            }

            await _produtoRepository.RemoverAsync(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
