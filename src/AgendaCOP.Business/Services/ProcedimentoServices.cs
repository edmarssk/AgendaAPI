using AgendaCOP.Business.Interfaces.Repository;
using AgendaCOP.Business.Interfaces.Services;
using AgendaCOP.Business.Models;
using AgendaCOP.Business.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCOP.Business.Services
{
    public class ProcedimentoServices : BaseServices, IProcedimentoServices
    {

        private readonly IProcedimentoRepository _procedimentoRepository;

        public ProcedimentoServices(IProcedimentoRepository procedimentoRepository,
            INotificador notificador): base(notificador)
        {
            _procedimentoRepository = procedimentoRepository;
        }

        public async Task Adicionar(Procedimento procedimento)
        {
            if (!ExecutarValidacao(new ProcedimentoValidator(), procedimento)) return;

            if(_procedimentoRepository.Buscar(p => p.Nome == procedimento.Nome).Result.Any())
            {
                Notificar("Já existe um procedimento com esse nome");
                return;
            }

            await _procedimentoRepository.Adicionar(procedimento);
        }

        public async Task Atualizar(Procedimento procedimento)
        {
            if (!ExecutarValidacao(new ProcedimentoValidator(), procedimento)) return;

            if (_procedimentoRepository.Buscar(p => p.Nome == procedimento.Nome && p.Id != procedimento.Id).Result.Any())
            {
                Notificar("Já existe um procedimento com esse nome");
                return;
            }

            await _procedimentoRepository.Atualizar(procedimento);
        }

        public async Task Remover(Guid id)
        {
             await _procedimentoRepository.Remover(id);
        }
    }
}
