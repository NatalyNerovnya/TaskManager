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
    //Do I realy need IUserService here??
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
            Session["tasks"] = taskService.GetAllByPredicate(t => t.Id == user.Id);
            //ViewBag.TaskAction = true;
            ViewBag.User = user;
            return PartialView("_TasksView", tasks.Tasks);
        }

        
        public ActionResult ShowFromUserTasks()
        {
            var user = userService.GetOneByPredicate(u => u.Login == User.Identity.Name);
            var tasks = taskService.GetAllByPredicate(t => t.FromUserId == user.Id).ToList().GetTasksViewModel();
            Session["tasks"] = taskService.GetAllByPredicate(t => t.Id == user.Id);
            //ViewBag.TaskAction = true;
            ViewBag.User = user;
            return PartialView("_TasksView", tasks.Tasks);
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
            Session["taskId"] = task.Id;
            taskService.Create(new TaskEntity
            {
                Id = task.Id,
                Name = task.Name,
                Missions = task.Missions,
                IsChecked = task.IsChecked,
                DateCreation = task.DateCreation,
                Description = task.Description,
                FromUserId = task.FromUserId,
                ToUserId = task.ToUserId//,
                //User = task.User,
                //User1 = task.User1   
            });
            Session["taskId"] = task.Id;
            
            return PartialView("_MissionMenu");

            
        }
        

        ////[HttpPost]
        ////public ActionResult Search(string search)
        ////{
        ////    if (User.Identity.IsAuthenticated)
        ////    {
        ////        var user = userService.GetAllByPredicate(u => u.Login == User.Identity.Name).FirstOrDefault();
        ////        var files = taskService.GetAllByPredicate(file => file.UserId == user.Id && file.IsDelete != true && file.Name.Contains(search)).ToList().GetTasksViewModel();
        ////        ViewBag.FileAction = false;
        ////        return PartialView("_FilesView", files);
        ////    }
        ////    return View("StartView", true);
        ////}


        ////[HttpPost]
        ////public ActionResult Upload(UploadFileViewModel upload)
        ////{
        ////    string currentUser = User.Identity.Name;
        ////    var user = userService.GetOneByPredicate(u => u.Login == currentUser);

        ////    if (ModelState.IsValid)
        ////    {
        ////        if (upload != null && upload.File.ContentLength > 0)
        ////        {
        ////            string fileName = Path.GetFileName(upload.File.FileName);
        ////            int size = upload.File.ContentLength;
        ////            var bllfile = new FileEntity()
        ////            {
        ////                UserId = user.Id,
        ////                Name = Path.GetFileNameWithoutExtension(upload.File.FileName),
        ////                Path = Server.MapPath("~/Storage/" + user.Login + "/" + fileName),
        ////                Type = Path.GetExtension(upload.File.FileName),
        ////                Size = upload.File.ContentLength
        ////            };
        ////            taskService.Create(bllfile);
        ////            upload.File.SaveAs(Server.MapPath("~/Storage/" + user.Login + "/" + fileName));
        ////        }
        ////    }
        ////    return RedirectToAction("Index");
        ////}


        ////[HttpPost]
        ////[ButtonNameAction]
        ////public ActionResult Load(int Id)
        ////{
        ////    var file = taskService.GetById(Id);
        ////    return File(file.Path, System.Net.Mime.MediaTypeNames.Application.Octet, file.Name);
        ////}


        ////[HttpPost]
        ////public void DeleteFile(int id)
        ////{
        ////    var file = taskService.GetOneByPredicate(f => f.Id == id);
        ////    taskService.Delete(file);
        ////}


        ////[HttpPost]
        ////public ActionResult InBox(int id)
        ////{
        ////    var file = taskService.GetAllByPredicate(f => f.Id == id).FirstOrDefault();
        ////    taskService.AddToCart(file);
        ////    int userId = file.UserId;
        ////    var files = taskService.GetAllByPredicate(f => f.UserId == userId && f.IsDelete == true).ToList().GetTasksViewModel();
        ////    return PartialView("_Box", files);
        ////}


        ////[HttpPost]
        ////public ActionResult Restore(int id)
        ////{
        ////    var file = taskService.GetOneByPredicate(f => f.Id == id);
        ////    taskService.Restore(file);
        ////    int userId = file.UserId;
        ////    var files = taskService.GetAllByPredicate(f => f.UserId == userId && f.IsDelete == false).ToList().GetTasksViewModel();
        ////    ViewBag.FileAction = true;
        ////    return PartialView("_FilesView", files);
        ////}


        ////[HttpPost]
        ////public ActionResult FileSearch(string name)
        ////{
        ////    var user = userService.GetOneByPredicate(u => u.Login == name);
        ////    if (user != null)
        ////    {
        ////        var files = user.Files.Where(f => f.IsOpen == true);
        ////        Session["files"] = files;
        ////        var openFiles = files.ToList().GetTasksViewModel();
        ////        ViewBag.FileAction = false;
        ////        return PartialView("_FilesView", openFiles);
        ////    }
        ////    return null;
        ////}

        ////[HttpPost]
        ////public ActionResult MakeOpen(int id)
        ////{
        ////    taskService.MakeOpen(id);
        ////    return RedirectToAction("Index");
        ////}

        //public ActionResult Sort(string sortOrder)
        //{
        ////    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
        ////    ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

        //    var tasks = Session["tasks"];
        //    var model = taskService.SortTasks((tasks as IEnumerable<TaskEntity>), sortOrder).ToList().GetTasksViewModel();
        ////    ViewBag.FileAction = false;
        //    return PartialView("_TasksView", model);
        //}













    }
}