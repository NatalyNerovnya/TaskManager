using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMissionService 
    {
        void MarkAsDone(MissionEntity entity);

        IEnumerable<MissionEntity> GetAllEntities();

        MissionEntity GetById(int id);

        MissionEntity GetOneByPredicate(Expression<Func<MissionEntity, bool>> predicates);

        IEnumerable<MissionEntity> GetAllByPredicate(Expression<Func<MissionEntity, bool>> predicates);

        void Create(MissionEntity entity);

        void Edit(MissionEntity entity);

        void Delete(MissionEntity entity);

    }
}
