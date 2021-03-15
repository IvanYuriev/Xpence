using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Scenario.AuthSrv.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] JObject credentials)
        {
            if (!TryAuthenticate(credentials, out var accessor))
            {
                return Unauthorized();
            }

            var content = CreateResultWithTokens(credentials);
            return content;
        }

        [HttpPost]
        public IActionResult RefreshToken([FromBody] string refreshToken)
        {
            //TODO: handle refresh token as well!
            var content = CreateResultWithTokens(new JObject());
            return Ok(content);
        }

        private IActionResult CreateResultWithTokens(JObject client)
        {
            //TODO: create tokens
            var accessToken = String.Empty;
            var refreshToken = String.Empty;

            var response = new JObject
            {
                ["AccessToken"] = accessToken,
                ["RefreshToken"] = refreshToken
            };
            return Content(response.ToString());
        }

        private bool TryAuthenticate(JToken credentials, out string token)
        {
            var username = credentials.Value<string>("Username");
            var password = credentials.Value<string>("Password");
            
            //TODO: validate and try to login

            token = String.Empty;
            return true;
        }
    }
}
