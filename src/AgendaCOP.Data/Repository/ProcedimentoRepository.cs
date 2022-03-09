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
    public class ProcedimentoRepository : Repository<Procedimento>, IProcedimentoRepository
    {
        public ProcedimentoRepository(MainDbContext context): base(context) { }

        public async Task<Procedimento> BuscarProcedimentoAgendas(Guid id)
        {
            return await Db.Procedimentos.AsNoTracking().Include(a => a.Agendas).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
