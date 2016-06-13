using BLL.Entities;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class MissionMapper
    {
        public static MissionEntity GetBllEntity(this DalMission dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new MissionEntity()
            {
                Id = dalEntity.Id,
                Name = dalEntity.Name,
                TaskId = dalEntity.TaskId,
                Description = dalEntity.Description,
                IsDone = dalEntity.IsDone
            };
        }

        public static DalMission GetDalEntity(this MissionEntity bllEntity)
        {
            return new DalMission()
            {
                Id = bllEntity.Id,
                Name = bllEntity.Name,
                TaskId = bllEntity.TaskId,
                Description = bllEntity.Description,
                IsDone = bllEntity.IsDone
            };
        }

    }
}

