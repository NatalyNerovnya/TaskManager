using BLL.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                User user = null;
                using (TaskManagerEntityModel db = new TaskManagerEntityModel())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == User.Identity.Name);
                }
                //var user = userService.GetOneByPredicate(u => u.Login == User.Identity.Name);
                var model = taskService.GetAllByPredicate(f => f.Id == user.Id).ToList();
                //Task model = null;
                //using (TaskManagerEntityModel db = new TaskManagerEntityModel())
                //{
                //    model = db.Tasks.FirstOrDefault(u => u.Id == user.Id);
                //}
                var viewModel = model.GetTasksViewModel();
                Session["tasks"] = model.Where(f => f.IsChecked != true);
                ViewBag.TaskAction = true;
                return View(viewModel);
            }
            return View("StartView");
        }


        //public ActionResult ShowUserTasks(int id)
        //{
        //    var tasks = taskService.GetAllByPredicate(t => t.Id == id).ToList().GetTasksViewModel();
        //    Session["tasks"] = taskService.GetAllByPredicate(t => t.Id == id);
        //    ViewBag.TaskAction = true;
        //    return PartialView("_TasksView", tasks);
        //}
        

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

        ////public ActionResult Sort(string sortOrder)
        ////{
        ////    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
        ////    ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

        ////    var files = Session["files"];
        ////    var model = taskService.SortFiles((files as IEnumerable<FileEntity>), sortOrder).ToList().GetTasksViewModel();
        ////    ViewBag.FileAction = false;
        ////    return PartialView("_FilesView", model);
        ////}
















        //public ActionResult Index()
        //{
        //    //string result = "Вы не авторизованы";
        //    //if (User.Identity.IsAuthenticated)
        //    //{
        //    //    result = "Ваш логин: " + User.Identity.Name;
        //    //}

        //    //return View(result);
        //    return View();
        //}
    }
}