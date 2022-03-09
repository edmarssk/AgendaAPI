using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaCOP.Business.Interfaces.Services;
using AgendaCOP.Business.Interfaces.Util;
using AgendaCOP.Business.Notificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgendaCOP.API.Controllers
{

    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificar;

        protected readonly IUser _user;

        protected bool UsuarioLogado;

        protected Guid IdUsuarioLogado;

        public MainController(INotificador notificador,
            IUser user)
        {
            _notificar = notificador;
            _user = user;

            if (_user.IsAuthenticated())
            {
                UsuarioLogado = true;
                IdUsuarioLogado = _user.GetUserId();
            }
        }

        protected bool OperacaoValida()
        {
            return !_notificar.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result  = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificar.ObterNotificacao().Select(m => m.Mensagem)
            });
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotificarErrosModalInvalid(modelState);

            return CustomResponse();
        }

        protected void NotificarErrosModalInvalid(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var item in errors)
            {
                var error = item.Exception == null ? item.ErrorMessage : item.Exception.Message;
                NotificarErro(error);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificar.Handle(new Notificacao(mensagem));
        }
    }
}
