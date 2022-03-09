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
    public class ClienteServices : BaseServices, IClienteServices
    {

        private readonly IClienteRepository _clienteRepository;

        private readonly IAgendaRepository _agendaRepository;

        private readonly IEnderecoRepository _enderecoRepository;

        public ClienteServices(IClienteRepository clienteRepository,
            IAgendaRepository agendaRepository,
            IEnderecoRepository enderecoRepository,
            INotificador notificador): base(notificador)
        {

            _clienteRepository = clienteRepository;
            _agendaRepository = agendaRepository;
            _enderecoRepository = enderecoRepository;
        }
        public async Task Adicionar(Cliente cliente)
        {
            //Maneira simples de fazer
            //var validador = new ClienteValidator();
            //var result = validador.Validate(cliente);
            //if (!result.IsValid)
            //{
            //    //Enviar erros
            //}

            //Melhor maneira de fazer
            if (!ExecutarValidacao(new ClienteValidator(), cliente)
                || !ExecutarValidacao(new EnderecoValidator(), cliente.Endereco)) return;

            if (_clienteRepository.Buscar(c => c.Cpf == cliente.Cpf).Result.Any())
            {
                Notificar("Já existe um cliente cadastrado com esse CPF");
                return;
            }
            await _clienteRepository.Adicionar(cliente);
        }

        public async Task Atualizar(Cliente cliente)
        {
            if (!ExecutarValidacao(new ClienteValidator(), cliente)) return;

            if (_clienteRepository.Buscar(c => c.Cpf == cliente.Cpf && c.Id != cliente.Id).Result.Any())
            {
                Notificar("Já existe um cliente cadastrado com esse CPF");
                return;
            }

            await _clienteRepository.Atualizar(cliente);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidator(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

      

        public async Task Remover(Guid id, Guid idEndereco)
        {
            if (_agendaRepository.BuscarAgendasCliente(id).Result.Any())
            {
                Notificar("Este cliente possui agendas, não é possivel excluir");
                return;
            }

            await _enderecoRepository.Remover(idEndereco);
            await _clienteRepository.Remover(id);
            await _clienteRepository.SaveChanges();
        
        }

        public void Dispose()
        {
            _agendaRepository?.Dispose();
            _clienteRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }

    }
}
