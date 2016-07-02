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
        public ActionResult Create(MissionViewModel model)
        {
            
            missionService.Create(new MissionEntity
            {
                Id = model.Id,
                TaskId = model.TaskId,
                Name = model.Name,
                IsDone = false,
                Description = model.Description
            });
            var mission = missionService.GetAllByPredicate(m => m.TaskId == model.TaskId).Select(m=>m.GetMissionViewModel
                ());
            //return PartialView("_MissionMenu");
            return PartialView("_MissionView", mission);
            
            //return RedirectToAction("ShowMission",new {id = model.TaskId});
        }

        [HttpGet]
        public ActionResult ShowMission(int id)
        {
            var task = taskService.GetById(id);
            var mission = task.Missions.Select(m => m.GetMissionViewModel()).ToList();
            return PartialView("_MissionView", mission);
        }

        [HttpPost]
        public ActionResult MarkAsDone(int id)
        {
            var mission = missionService.GetById(id);

            if (mission != null)
                missionService.MarkAsDone(mission);

            var missions = missionService.GetAllByPredicate(m=>m.Id == id).Select(m=>m.GetMissionViewModel()).ToList();
            return PartialView("_MissionView", missions);
        }

    }
}