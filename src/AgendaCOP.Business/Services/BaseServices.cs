using System;
using System.Collections.Generic;
using FluentValidation;
using System.Text;
using FluentValidation.Results;
using AgendaCOP.Business.Models;
using AgendaCOP.Business.Interfaces.Services;
using AgendaCOP.Business.Notificacoes;

namespace AgendaCOP.Business.Services
{
    public abstract class BaseServices
    {

        private readonly INotificador _notificador;

        protected BaseServices(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                Notificar(erro.ErrorMessage);
            }
        }
        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV,TE>(TV validacao,TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
