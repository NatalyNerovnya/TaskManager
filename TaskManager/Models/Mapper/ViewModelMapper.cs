using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models.Mapper
{
    public static class ViewModelMapper
    {
        public static TaskViewModel GetTaskViewModel(this TaskEntity bllEntity)
        {
            if (bllEntity == null)
                return null;
            return new TaskViewModel()
            {
                Id = bllEntity.Id,
                Name = bllEntity.Name,
                DateCreation = bllEntity.DateCreation,
                Description = bllEntity.Description,
                FromUserId = bllEntity.FromUserId,
                ToUserId = bllEntity.ToUserId,
                IsChecked = bllEntity.IsChecked,
                Missions = bllEntity.Missions,
                User = bllEntity.User,
                User1 = bllEntity.User1
            };
        }

        public static TasksViewModel GetTasksViewModel(this List<TaskEntity> listBllEntity)
        {
            TasksViewModel model = new TasksViewModel()
            {
                Tasks = listBllEntity.Where(t => t.IsChecked != true).Select(t => t.GetTaskViewModel()).ToList(),
                CheckedTasks = listBllEntity.Where(t => t.IsChecked == true).Select(t => t.GetTaskViewModel()).ToList(),

            };
            model.CountTasks = model.Tasks.Count;
            model.CountCheckedTasks= model.CheckedTasks.Count;
            return model;
        }
    }
}