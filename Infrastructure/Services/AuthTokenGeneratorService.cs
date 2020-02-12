using Core.Models.Auth;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class AuthTokenGeneratorService : IAuthTokenGeneratorService
    {
        public string GenerateToken(BasicAuthRequest basicAuthRequest)
        {
            string concartenatedCredentials = $"{basicAuthRequest.Username}:{basicAuthRequest.Password}";
            var basicAuthTextBytes = Encoding.UTF8.GetBytes(concartenatedCredentials);
            string encoded = Convert.ToBase64String(basicAuthTextBytes);
            return encoded;

        }
    }
}
