using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TaskManager.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        public DateTime DateCreation { get; set; }

        public bool IsChecked { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<MissionEntity> Missions { get; set; }

        public virtual UserEntity User { get; set; }

        public virtual UserEntity User1 { get; set; }
    }

    public class TasksViewModel
    {
        public List<TaskViewModel> Tasks;
        public List<TaskViewModel> CheckedTasks;
        public int CountTasks;
        public int CountCheckedTasks;
    }
}
