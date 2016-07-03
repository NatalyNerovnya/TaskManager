using BLL.Entities;
using BLL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.Models.Mapper;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskService taskService;
        private readonly IUserService userService;

        public HomeController(ITaskService taskService, IUserService userService)
        {
            this.taskService = taskService;
            this.userService = userService;
        }

        public ActionResult Index(string sortOrder)
        {
            if (User.Identity.IsAuthenticated)
            {   
                var user = userService.GetOneByPredicate(u => u.Login == User.Identity.Name);
                var tasks = taskService.GetAllByPredicate(t=>t.FromUserId == user.Id).ToList().GetTasksViewModel();
                ViewBag.Tasks = tasks;
                ViewBag.AllUsers = userService.GetAllEntities();
            }
            return View("StartView");
        }

        
        public ActionResult ShowUserTasks()
        {
            var user = userService.GetOneByPredicate(u => u.Login == User.Identity.Name);  
            var tasks = taskService.GetAllByPredicate(t => t.ToUserId == user.Id).ToList().GetTasksViewModel();
            ViewBag.User = user;
            ViewBag.Show = false;
            return PartialView("_TasksView", tasks.Tasks);
        }

        
        public ActionResult ShowFromUserTasks()
        {
            var user = userService.GetOneByPredicate(u => u.Login == User.Identity.Name);
            var tasks = taskService.GetAllByPredicate(t => t.FromUserId == user.Id).ToList().GetTasksViewModel();
            ViewBag.User = user;
            ViewBag.Show = true;
            return PartialView("_TasksView", tasks.Tasks);
        }

        public ActionResult ShowCheckedTasks()
        {
            var user = userService.GetOneByPredicate(u => u.Login == User.Identity.Name);
            var tasks = taskService.GetAllByPredicate(t => t.ToUserId == user.Id).ToList().GetTasksViewModel();
            ViewBag.User = user;
            ViewBag.Show = false;
            return PartialView("_TasksView", tasks.CheckedTasks);
        }

        public ActionResult ShowCheckedFromUserTasks()
        {
            var user = userService.GetOneByPredicate(u => u.Login == User.Identity.Name);
            var tasks = taskService.GetAllByPredicate(t => t.FromUserId == user.Id).ToList().GetTasksViewModel();
            ViewBag.User = user;
            ViewBag.Show = true;
            return PartialView("_TasksView", tasks.CheckedTasks);
        }

        [HttpGet]
        public ActionResult CreateTask()
        {
            ViewBag.AllUsers = userService.GetAllEntities();
            return PartialView("_TaskMenu");
        }

        [HttpPost]
        public ActionResult CreateTask(TaskViewModel task, string toUser)
        {
            var userId = userService.GetOneByPredicate(u=>u.Login == toUser);
            var thisUser = userService.GetOneByPredicate(u => u.Login == User.Identity.Name);
            task.FromUserId = thisUser.Id;
            task.ToUserId = userId.Id;
            
            int id = taskService.CreateTask(new TaskEntity
            {
                Name = task.Name,
                Missions = task.Missions,
                IsChecked = task.IsChecked,
                DateCreation = task.DateCreation,
                Description = task.Description,
                FromUserId = task.FromUserId,
                ToUserId = task.ToUserId
            });
            ViewBag.TaskIdNew = id;
            return PartialView("_MissionMenu");
           }
        
        public ActionResult MarkAsChecked(int id)
        {
            var task = taskService.GetById(id);
            if (task != null)
                taskService.MarkAsChecked(task);

            var tasks = taskService.GetAllByPredicate(m => m.Id == id).Select(m => m.GetTaskViewModel()).ToList();
            ViewBag.Show = true;
            return PartialView("_TasksView", tasks);
        }

        [HttpPost]
        public ActionResult TaskSearch(string name)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Show = false;
                var user = userService.GetOneByPredicate(u=>u.Login == User.Identity.Name);
                var tasks = taskService.GetAllByPredicate(t => (t.ToUserId == user.Id || t.FromUserId == user.Id ) && t.Name == name).ToList();
                    if(tasks != null)
                        return PartialView("_TasksView", tasks.Select(t => t.GetTaskViewModel()).ToList());
            }
            return null;
        }
   }
}