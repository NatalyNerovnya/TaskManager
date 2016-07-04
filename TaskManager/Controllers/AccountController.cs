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

namespace TaskManager.Controllers
{
    public class AccountController : Controller
    {
        private IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
                
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = userService.GetOneByPredicate(u => u.Login == model.UserName && u.Password == model.Password);
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

            return RedirectToAction("Index", "Home", model);
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
            var user = userService.GetOneByPredicate(u => u.Login == model.UserName);
            if(user != null)
            {
                ModelState.AddModelError("", "Error! Try another Login");
            }
            if (ModelState.IsValid)
            {
                var existUser = userService.GetOneByPredicate(u => u.Email == model.UserEmail && u.Login == model.UserName);
                if (existUser == null)
                {
                    userService.Create(new UserEntity
                    {
                        Email = model.UserEmail,
                        Password = model.UserPassword,
                        Login = model.UserName
                    });

                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        return RedirectToAction("Index", "Home");
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
