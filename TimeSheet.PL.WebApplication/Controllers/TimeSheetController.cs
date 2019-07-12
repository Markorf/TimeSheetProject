using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.PL.WebApplication.ViewModels.TimeSheet;

namespace TimeSheet.PL.WebApp.Controllers
{
    public class TimeSheetController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View(new TimeSheetViewModel());
        }
    }
}