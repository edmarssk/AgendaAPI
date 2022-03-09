using AgendaCOP.Business.Interfaces.Repository;
using AgendaCOP.Business.Models;
using AgendaCOP.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCOP.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(MainDbContext context): base(context) { }

        public async Task<Cliente> BuscarClienteAgendas(Guid id)
        {
            return await Db.Clientes.AsNoTracking().Include(a => a.Agendas)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> BuscarClienteAgendasEndereco(Guid id)
        {
            return await Db.Clientes.AsNoTracking().Include(a => a.Agendas)
                .Include(e => e.Endereco).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> BuscarClientesAgendas()
        {
            return await Db.Clientes.AsNoTracking().Include(a => a.Agendas).ToListAsync();
        }
    }
}
