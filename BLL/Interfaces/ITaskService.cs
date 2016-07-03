using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITaskService 
    {
        int CreateTask(TaskEntity taskEntity);

        void MarkAsChecked(TaskEntity taskEntity);

        IEnumerable<TaskEntity> GetAllEntities();

        TaskEntity GetById(int id);

        TaskEntity GetOneByPredicate(Expression<Func<TaskEntity, bool>> predicates);

        IEnumerable<TaskEntity> GetAllByPredicate(Expression<Func<TaskEntity, bool>> predicates);

        void Create(TaskEntity entity);

        void Edit(TaskEntity entity);

        void Delete(TaskEntity entity);
    }
}
