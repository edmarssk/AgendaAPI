using AgendaCOP.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCOP.Business.Interfaces.Repository
{
    public interface IClienteRepository: IRepository<Cliente>
    {
        Task<Cliente> BuscarClienteAgendas(Guid id);

        Task<Cliente> BuscarClienteAgendasEndereco(Guid id);

        Task<IEnumerable<Cliente>> BuscarClientesAgendas();
    }
}
