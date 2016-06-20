﻿using BLL.Interfaces;
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
            var users = service.GetAllEntities();
            ViewBag.Users = users;
            return PartialView("_UsersView");
        }

        public ActionResult MakeAdmin()
        {
            return View();
        }
    }
}