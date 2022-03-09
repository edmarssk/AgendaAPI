using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AgendaCOP.API.AppConfig;
using AgendaCOP.API.ViewModel;
using AgendaCOP.Business.Interfaces.Services;
using AgendaCOP.Business.Interfaces.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AgendaCOP.API.Controllers
{
    [ApiVersion("1.0")]
    //PARA VERSÃO VELHA QUE AVISA AO CLIENTE
    //[ApiVersion("1.0",Deprecated = true)]
    [Route("api/v{version:apiVersion}/conta")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        //private readonly IUser _user;
        public AuthController(INotificador notificador,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings,
            IUser user) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            // _user = user;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(UserVM userVM)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser()
            {
                UserName = userVM.Email,
                Email = userVM.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userVM.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(await GerarToken(userVM.Email));
            }

            foreach (var item in result.Errors)
            {
                NotificarErro(item.Description);
            }

            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserVM loginUserVM)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUserVM.Email, loginUserVM.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GerarToken(loginUserVM.Email));
            }

            if (result.IsLockedOut)
            {
                NotificarErro("O usuario excedeu o número de tentativas para login e seu usuário está bloqueado!");
                return CustomResponse(loginUserVM);
            }

            NotificarErro("Usuario ou Password inválidos");
            return CustomResponse(loginUserVM);
        }

        [HttpPost("resetar")]
        public async Task<ActionResult> ResetarSenha(string email)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, "Ed!123");


            if (result.Succeeded)
            {
                return Ok();
            }

            foreach (var item in result.Errors)
            {
                NotificarErro(item.Description);
            }

            return CustomResponse();
        }

        private async Task<LoginResponseVM> GerarToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var item in userRoles)
            {
                claims.Add(new Claim("role", item));
            }
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHanble = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHanble.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidaEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_appSettings.ExpiracaoHoras)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            });

            var encondingToken = tokenHanble.WriteToken(token);

            var response = new LoginResponseVM
            {
                AccessToken = encondingToken,
                ExpireIn = TimeSpan.FromHours(Convert.ToDouble(_appSettings.ExpiracaoHoras)).TotalSeconds,
                UserToken = new UserTokenVM
                {
                     Id = user.Id,
                      Email = user.Email,
                      Claims = claims.Select(c => new ClaimsVM { Value = c.Value, Type = c.Type })
                }
            };
            return response;
        }

        private static long ToUnixEpochDate(DateTime utcNow)
        => (long)Math.Round((utcNow.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
