using AgendaCOP.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCOP.Business.Interfaces.Repository
{
    public interface IEnderecoRepository: IRepository<Endereco>
    {
        Task<Endereco> BuscarEnderecoCliente(Guid ClienteId);
    }
}
