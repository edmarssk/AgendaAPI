using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.Extensions
{
    public class IdentityMensagensErrosPortugues: IdentityErrorDescriber
    {
        public override IdentityError InvalidEmail(string email) { return new IdentityError() { Code = nameof(InvalidEmail), Description = "E-mail inválido." }; }
        public override IdentityError ConcurrencyFailure() { return new IdentityError() { Code = nameof(ConcurrencyFailure), Description = "Falha de concorrencia." }; }
        public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = $"OCrreu uma falha desconhecida." }; }
        public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = "Password incorreto." }; }
        public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = "Token inválido." }; }
        public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "Já existe um usuário com esse login." }; }
        public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = $"Usuario '{userName}' é inválido, só pode conter letras e números." }; }
        public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = $"Usuario '{userName}' já existe" }; }
        public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = $"Email '{email}' já existe." }; }
        public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = $"Role '{role}' é inválida." }; }
        public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = $"Role '{role}' já está feita." }; }
        public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "Usuário já possui um password definido." }; }
        public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "O bloqueio não está permitido para este usuário." }; }
        public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"Usuário já está incluído na role '{role}'." }; }
        public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = $"Usuário não está na role '{role}'." }; }
        public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = $"Passwords deve conter no mínimo {length} characters." }; }
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Passwords deve ter no mínimo 1 caracter especial, Ex:(#$_@*)." }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "Passwords deve ter no mpinimo 1 dígito numérico ('0'-'9')." }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "Passwords deve ter no mínimo 1 dígito caixa baixa ('a'-'z')." }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "Passwords deve ter no mínimo 1 dígito caixa Alta ('A'-'Z')." }; }
    }
}
