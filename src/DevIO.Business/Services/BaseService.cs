using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace DevIO.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(INotificador notificador, IUnitOfWork unitOfWork)
        {
            _notificador = notificador;
            _unitOfWork = unitOfWork;

        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notificar(item.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TValidation, TEntity>(TValidation validacao, TEntity entidade) 
            where TValidation : AbstractValidator<TEntity> 
            where TEntity : Entity
        {
            var validator = validacao.Validate(entidade);
            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }

        protected async Task<bool> CommitAsync()
        {
            if (await _unitOfWork.CommitAsync()) return true;

            Notificar("Não foi possivel salvar os dados no banco");
            return false;
        }
    }
}
