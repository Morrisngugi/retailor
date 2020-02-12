using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Retailr3.Controllers
{
    public abstract class BaseController : Controller
    {

        public void Alert(string message, NotificationType notificationType, int timer)
        {

            string bgr = "white";
            List<string> backgrounds = new List<string> { "#429cb6", "#54b07d", "#EB5E28", "#F3BB45" }; //info, success, error, warning

            if (notificationType.Equals(NotificationType.info))
            {
                bgr = backgrounds[0];
            }
            if (notificationType.Equals(NotificationType.success))
            {
                bgr = backgrounds[1];
            }
            if (notificationType.Equals(NotificationType.error))
            {
                bgr = backgrounds[2];
            }
            if (notificationType.Equals(NotificationType.warning))
            {
                bgr = backgrounds[3];
            }

            //var msg1 = "Swal.fire({ background: '" + bgr + "', position: 'top', type: '" + notificationType + "', toast: true, title: '" + message + "', showConfirmButton: true, timer: " + timer + " })";
            var msg = "Swal.fire({ title: '" + notificationType.ToString().ToUpper() + "', html: '" + message + "', type: '" + notificationType + "', timer: '" + timer + "' })";
            TempData["notification"] = msg;
        }
    }
}

