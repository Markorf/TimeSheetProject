using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.PL.WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        [Route("categories")]
        public ActionResult Index()
        {
            return View();
        }
    }
}