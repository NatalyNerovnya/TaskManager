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
    public class MissionController : Controller
    {
        private IMissionService missionService;
        private ITaskService taskService;

        public MissionController(ITaskService taskService, IMissionService missionService)
        {
            this.taskService = taskService;
            this.missionService = missionService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("_MissionMenu");
        }

        [HttpPost]
        public ActionResult Create(MissionViewModel model, int taskIdNew)
        {
            var task = taskService.GetById(taskIdNew);
            
            missionService.Create(new MissionEntity
            {
                Name = task.Name,
                TaskId = taskIdNew,
                IsDone = false,
                Description = model.Description
            });
            var mission = missionService.GetAllByPredicate(m => m.TaskId == taskIdNew).Select(m => m.GetMissionViewModel
                ());
            ViewBag.Tasks = taskService.GetAllEntities();
            ViewBag.Precentage = 0;
            return PartialView("_MissionView", mission);
        }

        [HttpGet]
        public ActionResult ShowMission(int id)
        {
            var task = taskService.GetById(id);
            ViewBag.Tasks = taskService.GetAllEntities();
            if (!ReferenceEquals(task.Missions,null))
            {
                var mission = task.Missions.Select(m => m.GetMissionViewModel()).ToList();
                ViewBag.Precentage = GetPrecentage(id);
                return PartialView("_MissionView", mission);
            }
            return RedirectToAction("ShowFromUserTasks","Home");
        }

        [HttpPost]
        public ActionResult MarkAsDone(int id)
        {
            var mission = missionService.GetById(id);

            if (mission != null)
                missionService.MarkAsDone(mission);

            var missions = missionService.GetAllByPredicate(m=>m.Id == id).Select(m=>m.GetMissionViewModel()).ToList();
            ViewBag.Precentage = GetPrecentage(mission.TaskId);
            return PartialView("_MissionView", missions);
        }


        private double GetPrecentage(int id)
        {
            var task = taskService.GetById(id);
            var doneMissions = task.Missions.Where(m => m.IsDone == true).ToList();
            int done = doneMissions.Count;
            double all = task.Missions.Count;
            return ((double)done * 100.0) / all;
        }

    }
}