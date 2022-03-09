using AgendaCOP.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCOP.Business.Interfaces.Repository
{
    public interface IAgendaRepository: IRepository<Agenda>
    {
        Task<IEnumerable<Agenda>> BuscarAgendasCliente(Guid ClienteId);

        Task<IEnumerable<Agenda>> BuscarAgendasProcedimento(Guid ProcedimentoId);

        Task<Agenda> BuscarAgendaClienteProcedimento(Guid id);

        Task<IEnumerable<Agenda>> BuscarAgendasClienteProcedimento(DateTime dataInicial, DateTime dataFinal);

    }
}
