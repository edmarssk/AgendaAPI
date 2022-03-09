using AgendaCOP.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCOP.Business.Interfaces.Services
{
    public interface IProcedimentoServices
    {

        Task Adicionar(Procedimento procedimento);

        Task Remover(Guid id);

        Task Atualizar(Procedimento procedimento);
    }
}
