﻿using System.Web.Mvc;

namespace CenterParcs.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}