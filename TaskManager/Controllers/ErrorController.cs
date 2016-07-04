﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View("Error404");
        }
           
    }
}