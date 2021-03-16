using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Scenario.AuthSrv.Models;

namespace Scenario.AuthSrv.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1")]
    public class AuthController : ControllerBase
    {
        private JwtSecurityTokenHandler _handler;
        private IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _handler = new JwtSecurityTokenHandler();
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            if (!TryAuthenticate(credentials, out var accessor))
            {
                return Unauthorized();
            }

            var content = CreateResultWithTokens(credentials);
            return content;
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] JToken refreshToken)
        {
            //TODO: handle refresh token as well!
            var content = CreateResultWithTokens(new UserCredentials());
            return Ok(content);
        }

        private IActionResult CreateResultWithTokens(UserCredentials client)
        {
            //TODO: create tokens
            var accessToken = String.Empty;
            var refreshToken = String.Empty;

            var response = new JObject
            {
                ["AccessToken"] = CreateAccessToken("CLIENT_SUB"),
                ["RefreshToken"] = refreshToken
            };
            return Content(response.ToString());
        }

        private bool TryAuthenticate(UserCredentials credentials, out string token)
        {
            //var username = credentials.Value<string>("Username");
            //var password = credentials.Value<string>("Password");
            
            //TODO: validate and try to login

            token = String.Empty;
            return true;
        }

        private string CreateAccessToken(string clientSub)
        {
            var secret = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);
            var key = new SymmetricSecurityKey(secret);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //TODO: should be smth like: new SigningCredentials(new X509SecurityKey(cert), "RS256");
            var header = new JwtHeader(signingCredentials);

            var expirationDate = DateTime.Now.AddMinutes(30); //TODO: put settings from config
            var expirationDateInSeconds = EpochTime.GetIntDate(expirationDate);
            var payload = new JwtPayload
            {
                {
                    JwtRegisteredClaimNames.Exp, expirationDateInSeconds.ToString()
                },
                {
                    JwtRegisteredClaimNames.Sub, clientSub
                },
                {
                    JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()
                },
                {
                    JwtRegisteredClaimNames.Iss, "Xpence"
                }
            };
            var token = new JwtSecurityToken(header, payload);
            var tokenSigned = _handler.WriteToken(token);
            return tokenSigned;
        }
    }
}
