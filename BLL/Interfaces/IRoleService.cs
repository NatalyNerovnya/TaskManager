using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRoleService 
    {
        IEnumerable<RoleEntity> GetAllEntities();

        RoleEntity GetById(int id);

        RoleEntity GetOneByPredicate(Expression<Func<RoleEntity, bool>> predicates);

        IEnumerable<RoleEntity> GetAllByPredicate(Expression<Func<RoleEntity, bool>> predicates);

        void Create(RoleEntity entity);

        void Edit(RoleEntity entity);

        void Delete(RoleEntity entity);
    }
}
