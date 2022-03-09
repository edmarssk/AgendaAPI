using AgendaCOP.Business.Notificacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Business.Interfaces.Services
{
    public interface INotificador
    {
        bool TemNotificacao();

        List<Notificacao> ObterNotificacao();

        void Handle(Notificacao notificacao);
    }
}
