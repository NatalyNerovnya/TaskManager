using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using Helpers;
using DAL.DTO;
using ORM;
using DAL.Mappers;

namespace DAL.Concrete
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbContext context;

        public TaskRepository(DbContext uow)
        {
            if (uow == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            this.context = uow;
        }

        public IEnumerable<DalTask> GetAll()
        {
            return context.Set<Task>().Select(task => task.GetDalEntity());
        }

        public DalTask GetById(int key)
        {
            var task = context.Set<Task>().FirstOrDefault(f => f.Id == key);
            return task == null ? null : task.GetDalEntity();
        }

        public DalTask GetOneByPredicate(Expression<Func<DalTask, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();
        }

        public IEnumerable<DalTask> GetAllByPredicate(Expression<Func<DalTask, bool>> f)
        {
            var visitor = new MyExpressionVisitor<DalTask, Task>(Expression.Parameter(typeof(Task), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<Task, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return context.Set<Task>().Where(exp2).Select(task => task.GetDalEntity());
        }

        public void Create(DalTask dalTask)
        {
            context.Set<Task>().Add(dalTask.GetORMEntity());
        }

        public void Delete(DalTask dalTask)
        {
            context.Set<Task>().Attach(dalTask.GetORMEntity());
            context.Set<Task>().Remove(dalTask.GetORMEntity());
        }

        public void Update(DalTask dalTask)
        {
            context.Entry(dalTask.GetORMEntity()).State = EntityState.Modified;
        }
    }
}

