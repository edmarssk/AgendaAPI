using AgendaCOP.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCOP.Business.Interfaces.Services
{
    public interface IAgendaServices: IDisposable
    {
        Task Adicionar(Agenda agenda);

        Task Remover(Guid id);

        Task Atualizar(Agenda agenda);


    }
}
