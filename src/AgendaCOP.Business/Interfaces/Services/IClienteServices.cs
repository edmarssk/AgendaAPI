using AgendaCOP.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCOP.Business.Interfaces.Services
{
    public interface IClienteServices: IDisposable
    {
        Task Adicionar(Cliente cliente);

        Task Remover(Guid id, Guid idEndereco);

        Task Atualizar(Cliente cliente);

        Task AtualizarEndereco(Endereco endereco);
    }
}
