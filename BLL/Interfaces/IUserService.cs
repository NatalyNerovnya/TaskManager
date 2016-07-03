using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserEntity> GetAllEntities();

        UserEntity GetById(int id);

        UserEntity GetOneByPredicate(Expression<Func<UserEntity, bool>> predicates);

        IEnumerable<UserEntity> GetAllByPredicate(Expression<Func<UserEntity, bool>> predicates);

        void Create(UserEntity entity);

        void Edit(UserEntity entity);

        void Delete(UserEntity entity);
    }
}
