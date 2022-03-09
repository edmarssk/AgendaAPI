using AgendaCOP.Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaCOP.Business.Validations
{
    public class AgendaValidator: AbstractValidator<Agenda>
    {

        public AgendaValidator()
        {
            RuleFor(a => a.Data).NotEmpty().WithMessage("Campo {PropertyName} obrigatório");
        }
    }
}
