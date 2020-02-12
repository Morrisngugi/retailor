using Core.Services.Communications;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ISessionUserService
    {
        Task<ServiceResponse<UserInfoResponse>> GetUserInfo(string token, string infoUrl);
    }
}
