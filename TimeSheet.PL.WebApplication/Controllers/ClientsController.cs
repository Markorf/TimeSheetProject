using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.PL.WebApp.Controllers
{
    public class ClientsController : Controller
    {
        [Route("clients")]
        public ActionResult Index()
        {
            return View();
        }
    }
}