using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class RoleMapper
    {
        public static Role GetORMEntity(this DalRole dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new Role()
            {
                Name = dalEntity.Name
            };
        }

        public static DalRole GetDalEntity(this Role ormEntity)
        {
            return new DalRole()
            {
                Id = ormEntity.Id,
                Name = ormEntity.Name
            };
        }
    }
}
