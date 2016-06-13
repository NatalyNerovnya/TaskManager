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

        //public virtual ICollection<DalMission> Missions { get; set; }

        //public virtual DalUser User { get; set; }

        //public virtual DalUser User1 { get; set; }
    }
}