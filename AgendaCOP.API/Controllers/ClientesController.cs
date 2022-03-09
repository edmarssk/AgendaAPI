using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaCOP.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    //PARA VERSÃO VELHA QUE AVISA AO CLIENTE
    //[ApiVersion("1.0",Deprecated = true)]
    [Route("api/v{version:apiVersion}/clientes")]
    [ApiController]
    public class ClientesController : MainController
    {

        private readonly IClienteServices _clienteServices;
        private readonly IClienteRepository _clienteRepository;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;
        //private readonly IUser _user;
        public ClientesController(IClienteServices clienteServices,
            IClienteRepository clienteRepository,
            IMapper mapper,
            INotificador notificador,
            IUser user): base(notificador, user)
        {
            _clienteServices = clienteServices;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _notificador = notificador;
           // _user = user;
        }


        //[HttpGet("obter")]
        //public async Task<IEnumerable<ClienteVM>> ObterTodos()
        //{
        //    var retorno = _mapper.Map<IEnumerable<ClienteVM>>(await _clienteRepository.BuscarTodos());

        //    return retorno;
        //}

        [HttpGet("obter")]
        public async Task<ActionResult> ObterTodos()
        {
            var retorno = _mapper.Map<IEnumerable<ClienteVM>>(await _clienteRepository.BuscarTodos());

            foreach (var cliente in retorno)
            {
                if (System.IO.File.Exists(cliente.CaminhoFoto))
                {
                    cliente.Imagem = ConvertImagemBase64(cliente.CaminhoFoto);
                    cliente.NomeImagem = cliente.CaminhoFoto.Split("imagesPerfil\\")[1];
                }

            }

            return Ok(retorno);
        }

        [HttpGet("obter/{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            var cliente = await ObterClientePorId(id);

            if (cliente == null) return NotFound();

            return Ok(cliente);
        }

        [ClaimsAuthotize("Cliente","ins")]
        [HttpPost("cadastrar")]
        public async Task<ActionResult> Post(ClienteVM clienteVM)
        {
            clienteVM.DataCadastro = DateTime.Now;
            if (!ModelState.IsValid) return CustomResponse(ModelState);
           
            var nomeImagem = Guid.NewGuid() + clienteVM.NomeImagem;
            string caminhoFoto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagesPerfil", nomeImagem);

            if (!IncluirImagem(clienteVM.Imagem, caminhoFoto))
                return CustomResponse(ModelState);

            clienteVM.CaminhoFoto = caminhoFoto;

            await _clienteServices.Adicionar(_mapper.Map<Cliente>(clienteVM));

            return CustomResponse();
        }

        [RequestSizeLimit(40000000)]
        [HttpPost("adicionarimagem")]
        public async Task<ActionResult> AdicionarImagem(IFormFile image)
        {
            var id = Guid.NewGuid();
            var clienteVMAtual = await ObterClientePorId(id);

            if (clienteVMAtual == null)
            {
                NotificarErro("Cliente não encontrado com este ID");
                return CustomResponse();
            }

            if (image.Length == 0)
            {
                NotificarErro("Arquivo não enviado");
                return CustomResponse();
            }

            var nomeImagem = Guid.NewGuid() + ".jpg";
            var caminhoCompleto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagesPerfil", nomeImagem);

            if (!IncluirImagem(image, caminhoCompleto))
                return CustomResponse(ModelState);

            clienteVMAtual.CaminhoFoto = caminhoCompleto;

            await _clienteServices.Atualizar(_mapper.Map<Cliente>(clienteVMAtual));

            return CustomResponse();
        }

        [HttpPost("adicionarimagemform")]
        public async Task<ActionResult> AdicionarImagemForm(ClienteImagemVM clienteImagemVM)
        {
            clienteImagemVM.DataCadastro = DateTime.Now;
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (clienteImagemVM.Imagem.Length == 0)
            {
                NotificarErro("Arquivo não enviado");
                return CustomResponse();
            }

            var nomeImagem = Guid.NewGuid() + clienteImagemVM.NomeImagem;
            var caminhoCompleto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagesPerfil", nomeImagem);

            if (!IncluirImagem(clienteImagemVM.Imagem, caminhoCompleto))
                return CustomResponse(ModelState);

            clienteImagemVM.CaminhoFoto = caminhoCompleto;

            await _clienteServices.Adicionar(_mapper.Map<Cliente>(clienteImagemVM));

            return CustomResponse();
        }

        [ClaimsAuthotize("Cliente", "atu")]
        [HttpPut("editar/{id:guid}")]
        public async Task<ActionResult> Put(Guid id, ClienteVM clienteVM)
        {

            if (clienteVM.Id != id) return BadRequest();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (clienteVM.Imagem != null)
            {
                var nomeImagem = Guid.NewGuid() + clienteVM.NomeImagem;
                string novoCaminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagesPerfil", nomeImagem);

                var clienteVMAtual = await ObterClientePorId(id);

                if (!(AlterarImagem(clienteVM.Imagem, novoCaminhoImagem, clienteVMAtual.CaminhoFoto)))
                    return BadRequest();

                clienteVM.CaminhoFoto = novoCaminhoImagem;
            }

            await _clienteServices.Atualizar(_mapper.Map<Cliente>(clienteVM));

            return CustomResponse();
        }

        [ClaimsAuthotize("Cliente", "ins")]
        [HttpPut("editar-endereco/{id:guid}")]
        public async Task<ActionResult> Put(Guid id, EnderecoVM enderecoVM)
        {

            if (enderecoVM.Id != id) return BadRequest();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clienteServices.AtualizarEndereco(_mapper.Map<Endereco>(enderecoVM));

            return CustomResponse();
        }

        [HttpDelete("deletar-cliente/{id:guid}")]
        [ClaimsAuthotize("Cliente", "del")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var clienteVM = await ObterClientePorId(id);

            if (clienteVM == null) return NotFound();

            await _clienteServices.Remover(id, clienteVM.Endereco.Id);

            if (!_notificador.TemNotificacao())
                RemoverImagem(clienteVM.CaminhoFoto);

            return CustomResponse();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<ClienteVM> ObterClientePorId(Guid id)
        {
            return _mapper.Map<ClienteVM>(await _clienteRepository.BuscarClienteAgendasEndereco(id));
        }

        private bool IncluirImagem(string imagem, string caminhoImagem)
        {
            if (String.IsNullOrEmpty(imagem))
            {
                NotificarErro("Não foi fornecido uma imagem");
                return false;
            }
            if (System.IO.File.Exists(caminhoImagem))
            {
                NotificarErro("Já existe uma imagem similar");
                return false;
            }

            var bytes = Convert.FromBase64String(imagem);
            using (var imageFile = new FileStream(caminhoImagem, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }

            return true;
        }

        private bool IncluirImagem(IFormFile file, string caminhoCompleto)
        {
            if (System.IO.File.Exists(caminhoCompleto))
            {
                NotificarErro("Já existe um arquivo similar");
                return false;
            }

            using (var fs = new FileStream(caminhoCompleto, FileMode.Create))
            {
                file.CopyToAsync(fs);
            }

            return true;
        }

        private bool AlterarImagem(string imagem, string caminhoImagemNova, string caminhoCompletoImagemAntiga)
        {
            if (System.IO.File.Exists(caminhoImagemNova))
            {
                NotificarErro("Já existe um arquivo similar");
                return false;
            }

            var bytes = Convert.FromBase64String(imagem);
            using (var imageFile = new FileStream(caminhoImagemNova, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }

            if (!String.IsNullOrEmpty(caminhoCompletoImagemAntiga))
            {
                if (System.IO.File.Exists(caminhoCompletoImagemAntiga))
                    System.IO.File.Delete(caminhoCompletoImagemAntiga);
            }
            return true;
        }

        private string ConvertImagemBase64(string caminho)
        {
            using (Image image = Image.FromFile(caminho))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        private void RemoverImagem(string caminhoCompletoImagem)
        {
            if (System.IO.File.Exists(caminhoCompletoImagem))
                System.IO.File.Delete(caminhoCompletoImagem);
        }
    }
}
