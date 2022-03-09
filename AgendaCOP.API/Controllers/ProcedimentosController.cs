using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaCOP.API.Extensions;
using AgendaCOP.API.ViewModel;
using AgendaCOP.Business.Interfaces.Repository;
using AgendaCOP.Business.Interfaces.Services;
using AgendaCOP.Business.Interfaces.Util;
using AgendaCOP.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AgendaCOP.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    //PARA VERSÃO VELHA QUE AVISA AO CLIENTE
    //[ApiVersion("1.0",Deprecated = true)]
    [Route("api/v{version:apiVersion}/procedimentos")]
    [ApiController]
    public class ProcedimentosController : MainController
    {
        private readonly IProcedimentoServices _procedimentoServices;
        private readonly IProcedimentoRepository _procedimentoRepository;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;
        //private readonly IUser _user;

        public ProcedimentosController(IProcedimentoServices procedimentoServices,
            IProcedimentoRepository procedimentoRepository,
            IMapper mapper,
            INotificador notificador,
            IUser user): base(notificador, user)
        {
            _procedimentoServices = procedimentoServices;
            _procedimentoRepository = procedimentoRepository;
            _mapper = mapper;
            _notificador = notificador;
            //_user = user;
        }

        //[ClaimsAuthotize("Procedimento", "con")]
        [HttpGet]
        public async Task<IEnumerable<ProcedimentoVM>> ObterTodos()
        {
            var retorno = _mapper.Map<IEnumerable<ProcedimentoVM>>(await _procedimentoRepository.BuscarTodos());

            return retorno;
        }

        //[ClaimsAuthotize("Procedimento", "con")]
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProcedimentoVM), 200)]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            var procedimento = await ObterProcedimentoPorId(id);

            //Exemplo de como obter informações do usuário logado
            //if(UsuarioLogado)
            //{
            //    var emailUsuario = _user.GetUserEmail();
            //    var idUsuario = IdUsuarioLogado;
            //}

            if (procedimento == null) return NotFound();

            return Ok(procedimento);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProcedimentoVM procedimentoVM)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _procedimentoServices.Adicionar(_mapper.Map<Procedimento>(procedimentoVM));

            return CustomResponse(procedimentoVM);
        }

        [ClaimsAuthotize("Procedimento","atu")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, ProcedimentoVM procedimentoVM)
        {

            if (procedimentoVM.Id != id)
            {
                NotificarErro("O Id informado é diferente do Id do formulário");
                return CustomResponse(procedimentoVM);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _procedimentoServices.Atualizar(_mapper.Map<Procedimento>(procedimentoVM));

            return CustomResponse(procedimentoVM);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var procedimentoVM = await ObterProcedimentoPorId(id);

            if (procedimentoVM == null) return NotFound();

            await _procedimentoServices.Remover(id);

            return CustomResponse();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<ProcedimentoVM> ObterProcedimentoPorId(Guid id)
        {
            return _mapper.Map<ProcedimentoVM>(await _procedimentoRepository.BuscarPorId(id));
        }

    }
}
