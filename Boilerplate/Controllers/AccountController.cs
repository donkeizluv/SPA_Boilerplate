using DocumentArchiver.Auth;
using DocumentArchiver.Filter;
using DocumentArchiver.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DocumentArchiver.Controllers
{
    [CustomExceptionFilterAttribute]
    public class AccountController : Controller
    {
        private IConfiguration _config;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        internal string Issuer
        {
            get
            {
                return _config.GetSection("Authentication").GetValue<string>("Issuer");
            }
        }
        internal bool NoPwdCheck
        {
            get
            {
                return _config.GetSection("Authentication").GetValue<bool>("NoPwdCheck");
            }
        }
        
        internal string Domain
        {
            get
            {
                return _config.GetSection("Authentication").GetValue<string>("Domain");
            }
        }

        public AccountController(IConfiguration config,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            _config = config;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        public enum LoginResult
        {
            Error,
            NotActive,
            NoPermission,
            OK
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> DoLogin([FromForm]string username = "", [FromForm]string pwd = "")
        {
            ClearSession();
            //Check against db or whatever here
            var loginLevel = LoginResult.OK;
            //var loginLevel = GetLoginLevel(username, pwd, _context, out var user);
            if (loginLevel == LoginResult.Error) return Unauthorized();
            if (loginLevel == LoginResult.NoPermission) return Unauthorized();
            if (loginLevel == LoginResult.NotActive) return Unauthorized();
            //OK proceed
            //add claims to identity
            var userIdentity = new ClaimsIdentity("UserCred");
            //Add whatever claim user has here
            userIdentity.AddClaims(new[] { new Claim("sample claim", "123") });

            var jwt = await Token.GenerateJwt(userIdentity,
                _jwtFactory,
                "username",
                _jwtOptions,
                new JsonSerializerSettings { Formatting = Formatting.Indented });
            //Return token
            return new OkObjectResult(jwt);
        }
       
        private void ClearSession()
        {
            HttpContext.Session.Clear();
        }
        private bool Validate(string userName, string pwd)
        {
            if (NoPwdCheck) return true;
            return WindowsAuth.Validate_Principal2(userName, pwd, Domain);
        }
    }
}
