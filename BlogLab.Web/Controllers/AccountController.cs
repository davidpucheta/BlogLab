using BlogLab.Models.Account;
using BlogLab.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogLab.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUserIdentity> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUserIdentity> _userManager;
        public AccountController(ITokenService tokenService, UserManager<ApplicationUserIdentity> userManager, SignInManager<ApplicationUserIdentity> signInManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> Login(ApplicationUserLogin applicationUserLogin)
        {
            var applicationUserIdentity = await _userManager.FindByNameAsync(applicationUserLogin.Username);

            if (applicationUserIdentity == null)
                return BadRequest("Invalid login attempt.");

            var result = await _signInManager.CheckPasswordSignInAsync(applicationUserIdentity, applicationUserLogin.Password, false);

            if (result.Succeeded)
            {
                return Ok(new ApplicationUser
                {
                    ApplicationUserId = applicationUserIdentity.ApplicationUserId,
                    Username = applicationUserIdentity.Username,
                    Fullname = applicationUserIdentity.Fullname,
                    Email = applicationUserIdentity.Email,
                    Token = _tokenService.CreateToken(applicationUserIdentity)
                });
            }

            return BadRequest("Invalid login attempt.");
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> Register(ApplicationUserCreate applicationUserCreate)
        {
            var applicationUserIdentity = new ApplicationUserIdentity
            {
                Username = applicationUserCreate.Username,
                Email = applicationUserCreate.Email,
                Fullname = applicationUserCreate.Fullname
            };

            var result = await _userManager.CreateAsync(applicationUserIdentity, applicationUserCreate.Password);

            if (!result.Succeeded) 
                return BadRequest(result.Errors);

            return Ok(new ApplicationUser
            {
                ApplicationUserId = applicationUserIdentity.ApplicationUserId,
                Username = applicationUserIdentity.Username,
                Fullname = applicationUserIdentity.Fullname,
                Email = applicationUserIdentity.Email,
                Token = _tokenService.CreateToken(applicationUserIdentity)
            });
        }
    }
}
