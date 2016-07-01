using BLL.Entities;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.Models.Mapper;

namespace TaskManager.Controllers
{
    public class MissionController : Controller
    {
        private IMissionService missionService;
        private ITaskService taskService;

        //public MissionController(ITaskService taskService, IMissionService missionService)
        //{
        //    this.taskService = taskService;
        //    this.missionService = missionService;
        //}

        
        public ActionResult Create()
        {
            return View("_MissionMenu");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MissionViewModel model)
        {
            //missionService.Create(new MissionEntity
            //{
            //    Id = model.Id,
            //    TaskId = model.TaskId,
            //    Name = model.Name,
            //    IsDone = false,
            //    Description = model.Description
            //});

            //return PartialView("_MissionMenu");
            //return PartialView("_MissionView");
            return RedirectToAction("ShowMissions",new {id = model.TaskId});
        }

        public ActionResult ShowMissions(int id)
        {
            var task = taskService.GetById(id);
            var mission = missionService.GetAllByPredicate(m=>m.TaskId == task.Id).ToList();
            return PartialView("_MissionView", mission);
        }

        [HttpGet]
        public ActionResult MarkAsDone()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MarkAsDone(int id)
        {
            var mission = missionService.GetById(id);

            if (mission != null)
                missionService.MarkAsDone(mission);
            int taskId = mission.TaskId;
            var tasks = missionService.GetAllByPredicate(m => m.TaskId == taskId && m.IsDone == false).ToList().GetMissionsViewModel();

            ViewBag.TaskAction = true;
            return PartialView("_MissionsView", tasks);
        }

    }
}