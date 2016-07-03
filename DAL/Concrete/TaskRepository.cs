using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Migrations;
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
            var tasks = context.Set<Task>().ToList();
            return tasks.Select(task => task.GetDalEntity());
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
            var tasks = context.Set<Task>().Where(exp2).ToList();
            return tasks.Select(task => task.GetDalEntity());
        }

        public void Create(DalTask dalTask)
        {
            context.Set<Task>().Add(dalTask.GetORMEntity());
        }


        public int CreateTask(DalTask dalTask)
        {
            context.Set<Task>().Add(dalTask.GetORMEntity());
            return context.Set<Task>().Max(t=>t.Id) + 1;
        }

        public void Delete(DalTask dalTask)
        {
            context.Set<Task>().Attach(dalTask.GetORMEntity());
            context.Set<Task>().Remove(dalTask.GetORMEntity());
        }

        public void Update(DalTask dalTask)
        {
            context.Set<ORM.Task>().AddOrUpdate(dalTask.GetORMEntity());
            context.SaveChanges();
        }
    }
}

