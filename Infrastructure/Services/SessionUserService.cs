using Core.Services;
using Core.Services.Communications;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SessionUserService : ISessionUserService
    {
        public async Task<ServiceResponse<UserInfoResponse>> GetUserInfo(string token, string infoUrl)
        {
            try
            {
                var client = new HttpClient();

                var response = await client.GetUserInfoAsync(new UserInfoRequest
                {
                    Address = infoUrl,
                    Token = token
                });
                return new  ServiceResponse<UserInfoResponse>(response);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<UserInfoResponse>($"An Error Occured While Retrieving User Information. {ex.Message}" );
            }
  
        }

        
    }
}
