using AgendaCOP.Business.Interfaces.Repository;
using AgendaCOP.Business.Models;
using AgendaCOP.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCOP.Data.Repository
{
    public class AgendaRepository : Repository<Agenda>, IAgendaRepository
    {
        public AgendaRepository(MainDbContext context): base(context) { }

        public async Task<Agenda> BuscarAgendaClienteProcedimento(Guid id)
        {
            return await Db.Agendas.AsNoTracking().Include(a => a.Cliente)
                .Include(p => p.Procedimento)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Agenda>> BuscarAgendasProcedimento(Guid ProcedimentoId)
        {
            return await Db.Agendas.AsNoTracking().Include(p => p.Procedimento)
                .Where(a => a.ProcedimentoId == ProcedimentoId).ToListAsync();
        }

        public async Task<IEnumerable<Agenda>> BuscarAgendasCliente(Guid ClienteId)
        {
            try
            {
                return await Db.Agendas.AsNoTracking().Include(p => p.Procedimento)
                    .Where(a => a.ClienteId == ClienteId).ToListAsync();
            }
            catch(Exception erro)
            {
                throw erro;
            }
        }

        public async Task<IEnumerable<Agenda>> BuscarAgendasClienteProcedimento(DateTime dataInicial, DateTime dataFinal)
        {
            return await Db.Agendas.AsNoTracking().Include(c => c.Cliente)
                .Include(p => p.Procedimento)
                .Where(a => a.Data >= dataInicial && a.Data <= dataFinal).OrderByDescending(a => a.Data).ToListAsync();
        }
    }
}
