using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }

        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        public DateTime DateCreation { get; set; }

        public bool IsChecked { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<MissionEntity> Missions { get; set; }

        //public virtual UserEntity User { get; set; }

        //public virtual UserEntity User1 { get; set; }
    }
}
