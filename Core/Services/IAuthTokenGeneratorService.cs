using Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface IAuthTokenGeneratorService
    {
        string GenerateToken(BasicAuthRequest basicAuthRequest);
    }
}
