using AgendaCOP.Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Business.Validations
{
    public class ProcedimentoValidator: AbstractValidator<Procedimento>
    {
        public ProcedimentoValidator()
        {
            RuleFor(p => p.Nome).NotEmpty().WithMessage("Campo {PropertName} obrigatório");
            RuleFor(p => p.Descricao).NotEmpty().WithMessage("Campo {PropertyName} obrigatório");
            RuleFor(p => p.Valor).GreaterThan(0).WithMessage("Campo {PropertyName} deve ser maior que 0");

        }
    }
}
