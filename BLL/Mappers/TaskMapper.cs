using BLL.Entities;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class TaskMapper
    {
        public static TaskEntity GetBllEntity(this DalTask dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new TaskEntity()
            {
                Id = dalEntity.Id,
                IsChecked = dalEntity.IsChecked,
                FromUserId = dalEntity.FromUserId,
                ToUserId = dalEntity.ToUserId,
                DateCreation = dalEntity.DateCreation,
                Name = dalEntity.Name,
                Description = dalEntity.Description,
                User = dalEntity.User.GetBllEntity(),
                User1 = dalEntity.User1.GetBllEntity(),
                Missions = dalEntity.Missions != null
                ? dalEntity.Missions.Select(m => m.GetBllEntity()).ToList()
                : null               
            };
        }

        public static DalTask GetDalEntity(this TaskEntity bllEntity)
        {
            return new DalTask()
            {
                Id = bllEntity.Id,
                IsChecked = bllEntity.IsChecked,
                FromUserId = bllEntity.FromUserId,
                ToUserId = bllEntity.ToUserId,
                DateCreation = bllEntity.DateCreation,
                Name = bllEntity.Name,
                Description = bllEntity.Description,
                User = bllEntity.User.GetDalEntity(),
                User1 = bllEntity.User1.GetDalEntity(),
                Missions = bllEntity.Missions.Select(m => m.GetDalEntity()).ToList(),  
            };
        }

    }
}

