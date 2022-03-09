using AgendaCOP.Business.Interfaces.Repository;
using AgendaCOP.Business.Models;
using AgendaCOP.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AgendaCOP.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MainDbContext mainDbContext) : base(mainDbContext)
        {
        }

        public async Task<Endereco> BuscarEnderecoCliente(Guid ClienteId)
        {
            return await Db.Enderecos.AsNoTracking().FirstOrDefaultAsync(e => e.ClienteId == ClienteId);
        }
    }
}
