using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            ViewBag.Users = service.GetAllEntities();
            ViewBag.User = service.GetOneByPredicate(u => u.Login == User.Identity.Name);
            return PartialView("_UsersView");
        }

    }
}