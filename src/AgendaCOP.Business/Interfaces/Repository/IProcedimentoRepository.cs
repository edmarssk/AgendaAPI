using AgendaCOP.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCOP.Business.Interfaces.Repository
{
    public interface IProcedimentoRepository: IRepository<Procedimento>
    {
        Task<Procedimento> BuscarProcedimentoAgendas(Guid id);
    }
}
