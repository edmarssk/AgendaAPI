using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaCOP.API.ViewModel;
using AgendaCOP.Business.Interfaces.Repository;
using AgendaCOP.Business.Interfaces.Services;
using AgendaCOP.Business.Interfaces.Util;
using AgendaCOP.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaCOP.API.Controllers
{

    [Authorize]
    [ApiVersion("1.0")]
    //PARA VERSÃO VELHA QUE AVISA AO CLIENTE
    //[ApiVersion("1.0",Deprecated = true)]
    [Route("api/v{version:apiVersion}/agendas")]
    //[Route("api/[controller]")]
    [ApiController]
    public class AgendasController : MainController
    {

        private readonly IAgendaServices _agendaServices;
        private readonly IAgendaRepository _agendaRepository;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;


        public AgendasController(IAgendaServices agendaServices,
            IAgendaRepository agendaRepository,
            IMapper mapper,
            INotificador notificador,
            IUser user) : base(notificador, user)
        {
            _agendaServices = agendaServices;
            _agendaRepository = agendaRepository;
            _mapper = mapper;
            _notificador = notificador;
            //_user = user;
        }

        [HttpGet]
        public async Task<IEnumerable<AgendaVM>> ObterTodos()
        {
            var retorno = _mapper.Map<IEnumerable<AgendaVM>>(await _agendaRepository.BuscarTodos());

            return retorno;
        }

        //[ClaimsAuthotize("Procedimento", "con")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            var agendas = await ObterAgendaPorId(id);

            if (agendas == null) return NotFound();

            return Ok(agendas);
        }

        [HttpGet("obterporcliente/{id:guid}")]

        public async Task<ActionResult> ObterPorClienteId(Guid id)
        {
            var agendas = await ObterAgendasPorClienteId(id);

            if (agendas == null) return NotFound();

            return Ok(agendas);
        }

        [HttpGet("obterporprocedimento/{id:guid}")]
        public async Task<ActionResult> ObterPorProcedimentoId(Guid id)
        {
            var agendas = await ObterAgendasPorProcedimentoId(id);

            if (agendas == null) return NotFound();

            return Ok(agendas);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AgendaVM agendaVM)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _agendaRepository.Adicionar(_mapper.Map<Agenda>(agendaVM));

            return CustomResponse(agendaVM);
        }

        
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, AgendaVM agendaVM)
        {

            if (agendaVM.Id != id)
            {
                NotificarErro("O Id informado é diferente do Id do formulário");
                return CustomResponse(agendaVM);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _agendaRepository.Atualizar(_mapper.Map<Agenda>(agendaVM));

            return CustomResponse(agendaVM);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var procedimentoVM = await ObterAgendaPorId(id);

            if (procedimentoVM == null) return NotFound();

            await _agendaRepository.Remover(id);

            return CustomResponse();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<AgendaVM> ObterAgendaPorId(Guid id)
        {
            return _mapper.Map<AgendaVM>(await _agendaRepository.BuscarPorId(id));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<IEnumerable<AgendaVM>> ObterAgendasPorClienteId(Guid id)
        {
            return _mapper.Map<IEnumerable<AgendaVM>>(await _agendaRepository.BuscarAgendasCliente(id));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<IEnumerable<AgendaVM>> ObterAgendasPorProcedimentoId(Guid id)
        {
            return _mapper.Map<IEnumerable<AgendaVM>>(await _agendaRepository.BuscarAgendasProcedimento(id));
        }
    }
}
