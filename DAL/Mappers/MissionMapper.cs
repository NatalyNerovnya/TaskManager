using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class MissionMapper
    {
        public static Mission GetORMEntity(this DalMission dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new Mission()
            {
                Id = dalEntity.Id,
                Name = dalEntity.Name,
                TaskId = dalEntity.TaskId,
                Description = dalEntity.Description,
                IsDone = dalEntity.IsDone
            };
        }

        public static DalMission GetDalEntity(this Mission ormEntity)
        {
            return new DalMission()
            {
                Id = ormEntity.Id,
                Name = ormEntity.Name,
                TaskId = ormEntity.TaskId,
                Description = ormEntity.Description,
                IsDone = ormEntity.IsDone
            };
        }

    }
}

