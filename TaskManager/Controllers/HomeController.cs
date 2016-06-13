using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //string result = "Вы не авторизованы";
            //if (User.Identity.IsAuthenticated)
            //{
            //    result = "Ваш логин: " + User.Identity.Name;
            //}
           
            //return View(result);
            return View();
        }
    }
}