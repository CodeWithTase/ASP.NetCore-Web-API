using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FootballTeamInfo.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }

        private class FootballTeamUser
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FootballTeam { get; set; }

            public FootballTeamUser(
                int userId,
                string userName,
                string firstName,
                string lastName,
                string footballTeam)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                FootballTeam = footballTeam;
            }

        }
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ?? 
                throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate
            (AuthenticationRequestBody authenticationRequestBody)
        {
            //validate user credentials
            var user = ValidateUserCredentials
                (authenticationRequestBody.UserName, authenticationRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            //create a token
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForkey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            //This claims that
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));
            claimsForToken.Add(new Claim("footballTeam", user.FootballTeam));

            var jwtsecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtsecurityToken);

            return Ok(tokenReturn);

        }

        private FootballTeamUser ValidateUserCredentials(string? userName, string? password)
        {
            // this scenario doesnt have a user db or table. If you have check that details passed through validates against what is in the db
            // for demo we assume details are valid
            //return FootballTeamUser values would normally come from db

            return new FootballTeamUser(1, userName ?? "", "Petey", "Pye", "Arsenal");
        }
    }
}
