using BLL.Entities;
using BLL.Interfaces;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskManager.Infrastructura.Provider;
using TaskManager.Models.AccauntViewModel;
using ORM;

namespace TaskManager.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        //public AccountController()
        //{
        //    //_userService = userService;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                //var user = _userService.GetOneByPredicate(u => u.Email == model.UserName && u.Password == model.Password);
                //var user = _userService.GetById(1);
                User user = null;
                using (TaskManagerEntityModel db = new TaskManagerEntityModel())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.UserName && u.Password == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong password or login");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = _userService.GetOneByPredicate(u => u.Email == model.UserName);
                User user = null;
                using (TaskManagerEntityModel db = new TaskManagerEntityModel())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.UserName && u.Password == model.UserPassword);
                }
                if (user == null)
                {
                    
                    // создаем нового пользователя
                    _userService.Create(new UserEntity
                    {
                        Email = model.UserEmail,
                        Password = model.UserPassword,
                        Login = model.UserName,
                        Id = model.UserId
                    });
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error registration");
                }
            }

            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
