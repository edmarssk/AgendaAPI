using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Business.Notificacoes
{
    public class Notificacao
    {
        public Notificacao(string mensagem)
        {
            this.Mensagem = mensagem;
        }
        public string Mensagem { get;  }
    }
}
