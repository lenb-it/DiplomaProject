using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Business.Constants;
using ServerApi.DbModels;
using Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace ServerApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtOptions _jwtOptions;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        /// <remarks>
        /// Пользователь, Админ, Работник:
        ///
        ///     POST /Todo
        ///     {
        ///        "email": "user@example.com",
        ///        "password": "string00";
        ///        "email": "admin@mail.ru",
        ///        "password": "secretpas123";
        ///        "email": "workerAnton@mail.ru",
        ///        "password": "123qweasd".
        ///     }
        ///     
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] LogInViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            //if (!model.IsValid()) return BadRequest(model.Errors);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) return Unauthorized();

            var resultSignIn = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!resultSignIn.Succeeded) return BadRequest(new[] { "error password" });

            var claims = (await _userManager.GetClaimsAsync(user)).ToList();
            var token = CreateToket(claims);

            return Ok(new { user.Email, token });
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            //if (!model.IsValid()) return BadRequest(model.Errors);

            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return BadRequest("Такой пользователь уже зарегестрирован");

            var registerUser = CreateRegisterUser(model);
            var createResult = await _userManager.CreateAsync(registerUser, model.Password);

            if (!createResult.Succeeded) return BadRequest(createResult.Errors);

            var user = await _userManager.FindByEmailAsync(registerUser.Email);
            await _userManager.AddToRoleAsync(user, RoleNames.User);

            var claims = CreateUserClaims(user);
            await _userManager.AddClaimsAsync(user, claims);
            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            var token = CreateToket(claims);

            return signInResult.Succeeded ? Ok(token) : Unauthorized();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CheckToken()
        {
            return Ok();
        }

        private string CreateToket(List<Claim> claims)
        {
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(_jwtOptions.Expire)),
                signingCredentials: new SigningCredentials(_jwtOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private User CreateRegisterUser(RegisterViewModel model)
        {
            return new User
            {
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                LastName = model.LastName,
                FirstName = model.FirstName,
                CreatedDate = DateTime.UtcNow,
            };
        }

        private List<Claim> CreateUserClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, RoleNames.User),
            };
        }
    }
}
