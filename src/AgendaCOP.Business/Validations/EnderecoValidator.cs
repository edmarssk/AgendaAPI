using AgendaCOP.Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Business.Validations
{
    public class EnderecoValidator: AbstractValidator<Endereco>
    {
        public EnderecoValidator()
        {
            RuleFor(e => e.Logradouro).NotEmpty().WithMessage("Campo {PropertyName} obrigatório")
                .Length(3,100).WithMessage("Campo Logradouro deve ter entre 3 e 100 caracteres");

            RuleFor(e => e.Numero).NotEmpty().WithMessage("Campo {PropertyName} obrigatório")
                .Length(1, 15).WithMessage("Campo número deve ter entre 1 e 15 caracteres");

            RuleFor(e => e.Bairro).NotEmpty().WithMessage("Campo {PropertyName} obrigatório")
                .Length(3, 100).WithMessage("Campo bairro deve ter entre 3 e 100 caracteres");

            RuleFor(e => e.Cidade).NotEmpty().WithMessage("Campo {PropertyName} obrigatório")
                .Length(3, 100).WithMessage("Campo cidade deve ter entre 3 e 100 caracteres");

            RuleFor(e => e.Estado).NotEmpty().WithMessage("Campo {PropertyName} obrigatório")
                .Length(2).WithMessage("Campo estado deve ter 2 caracteres");

            RuleFor(e => e.Cep).NotEmpty().WithMessage("Campo {PropertyName} obrigatório")
                .Length(8).WithMessage("Campo CEP deve ter 8 caracteres");
        }
    }
}
