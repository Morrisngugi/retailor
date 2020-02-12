using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Core.Models;

using Core.Services;
using Core.Utilities;
using IdentityModel.Client;
using Retailr3.Helpers;
using Retailr3.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Retailr3.Controllers
{
    [SecurityHeaders]
    [ResponseCache(CacheProfileName = "NoCache")]
    public class AccountController : BaseController
    {
        private readonly IOptions<AppConfig> _appConfig;

        public AccountController(IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string returnUrl = null)
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Landing()
        {

            var accessToken = HttpContext.GetTokenAsync("access_token");
            //var profile = GetUserInfo(accessToken.Result);

            if (accessToken.Result != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        public IActionResult TermsAndConditions()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogoutAsync()
        {
            string returnUrl = _appConfig.Value.NotifyrBaseUrl + "Account/Landing";
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return new SignOutResult(new[] { "Cookies", "oidc" }, new AuthenticationProperties { RedirectUri = returnUrl });
            }
            else
            {
                return new SignOutResult(new[] { "Cookies", "oidc" }, new AuthenticationProperties { RedirectUri = returnUrl });
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        
        #region Methods
        private IActionResult RedirectToUrl(string fallbackAction, string fallbackController, string redirectUrl)
        {
            if (Url.IsLocalUrl(redirectUrl))
            {
                return Redirect(redirectUrl);
            }
            else
            {
                return RedirectToAction(fallbackAction, fallbackController);
            }
        }

        private async Task<UserInfoResponse> GetUserInfo(string token)
        {
            var client = new HttpClient();

            var response = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = _appConfig.Value.Authority + "/connect/userinfo",
                Token = token
            });

            return response;
        }
        #endregion
    }
}