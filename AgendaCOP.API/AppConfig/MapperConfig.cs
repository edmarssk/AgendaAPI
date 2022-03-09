using AgendaCOP.API.ViewModel;
using AgendaCOP.Business.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.AppConfig
{
    public class MapperConfig: Profile 
    {
       public MapperConfig()
        {
            CreateMap<Cliente, ClienteVM>().ReverseMap();

            CreateMap<ClienteImagemVM, Cliente>();

            CreateMap<Endereco, EnderecoVM>().ReverseMap();

            CreateMap<AgendaVM, Agenda>();

            CreateMap<Agenda, AgendaVM>().ForMember(dest => dest.ProcedimentoDescricao, opt => opt.MapFrom(src => src.Procedimento.Descricao));

            CreateMap<Procedimento, ProcedimentoVM>().ReverseMap();
        }
    }
}
