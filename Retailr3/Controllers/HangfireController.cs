using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Retailr3.Controllers
{
    public class HangfireController : Controller
    {
        // GET: Hangfire
        public ActionResult Index()
        {
            return View();
        }

        
    }
}