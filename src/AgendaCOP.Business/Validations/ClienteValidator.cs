using AgendaCOP.Business.Models;
using AgendaCOP.Business.Models.Enum;
using AgendaCOP.Business.Validations.Helper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Business.Validations
{
    public class ClienteValidator: AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.Cpf.Length).Equal(11).WithMessage("Campo CPF possui {PropertyName} caracteres, mas deve ter 11");
            RuleFor(c => ValidarDocumento.IsCpf(c.Cpf)).Equal(true).WithMessage("Campo CPF inválido");
            RuleFor(c => c.DataNascimento).NotNull().WithMessage("Campo {PropertyName} obrigatório");
            RuleFor(c => c.Nome).NotNull().Length(5, 100).WithMessage("Campo {PropertyName} deve ser entre 5 e 100 caracteres");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Campo {PropertyName} inválido");

            When(c => c.Plano == TipoPlano.Mensal, () => {
                RuleFor(c => c.DataVencimentoPlano).NotNull().WithMessage("Campo {PropertyName} obrigatório");
            });

            

        }
    }
}
