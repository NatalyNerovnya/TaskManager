using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using ORM;
using DAL.DTO;

namespace DAL.Mappers
{
    public static class TaskMapper
    {
        public static Task GetORMEntity(this DalTask dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new Task()
            {
                Id = dalEntity.Id,
                IsChecked = dalEntity.IsChecked,
                FromUserId = dalEntity.FromUserId,
                ToUserId = dalEntity.ToUserId,
                DateCreation = dalEntity.DateCreation,
                Name = dalEntity.Name,
                Description = dalEntity.Description,
                User = dalEntity.User.GetORMEntity(),
                User1 = dalEntity.User1.GetORMEntity(),
                Missions = dalEntity.Missions != null
                ? dalEntity.Missions.Select(m => m.GetORMEntity()).ToList()
                : null
            };
        }

        public static DalTask GetDalEntity(this Task ormEntity)
        {
            return new DalTask()
            {
                Id = ormEntity.Id,
                IsChecked = ormEntity.IsChecked,
                FromUserId = ormEntity.FromUserId,
                ToUserId = ormEntity.ToUserId,
                DateCreation = ormEntity.DateCreation,
                Name = ormEntity.Name,
                Description = ormEntity.Description,
                User = ormEntity.User.GetDalEntity(),
                User1 = ormEntity.User1.GetDalEntity(),
                Missions = ormEntity.Missions.Select(m => m.GetDalEntity()).ToList(),
            };
        }

    }
}
