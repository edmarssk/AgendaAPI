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
    public class AgendaServices : BaseServices, IAgendaServices
    {
        private readonly IAgendaRepository _agendaRepository;

        public AgendaServices(IAgendaRepository agendaServices, INotificador notificador) : base(notificador)
        {
            _agendaRepository = agendaServices;
        }

        public async Task Adicionar(Agenda agenda)
        {
            if (!ExecutarValidacao(new AgendaValidator(), agenda)) return;

            if(_agendaRepository.Buscar(a => a.Data == agenda.Data).Result.Any())
            {
                Notificar("Já existe uma agenda com essa data e hora");
            }

            await _agendaRepository.Adicionar(agenda);
        }

        public async Task Atualizar(Agenda agenda)
        {
            if (!ExecutarValidacao(new AgendaValidator(), agenda)) return;

            if (_agendaRepository.Buscar(a => a.Data == agenda.Data && a.Id != agenda.Id).Result.Any())
            {
                Notificar("Já existe uma agenda com essa data e hora");
            }

            await _agendaRepository.Atualizar(agenda);
        }

        public async Task Remover(Guid id)
        {
            await _agendaRepository.Remover(id);
        }

        public void Dispose()
        {
            _agendaRepository?.Dispose();
        }
    }
}
