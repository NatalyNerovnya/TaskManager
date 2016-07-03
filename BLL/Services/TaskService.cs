using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO;
using DAL.Interfaces;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork uow;

        private readonly ITaskRepository taskRepository;


        public TaskService(IUnitOfWork uow, ITaskRepository taskRepository)
        {
            this.uow = uow;
            this.taskRepository = taskRepository;
        }

        public IEnumerable<TaskEntity> GetAllEntities()
        {
            var tasks = taskRepository.GetAll();
            return tasks.Select(task => task.GetBllEntity()).ToList();
        }

        public TaskEntity GetById(int id)
        {
            return taskRepository.GetById(id).GetBllEntity();
        }

        public TaskEntity GetOneByPredicate(Expression<Func<TaskEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<TaskEntity, DalTask>(Expression.Parameter(typeof(DalTask), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalTask, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return taskRepository.GetOneByPredicate(exp2).GetBllEntity();
        }

        public IEnumerable<TaskEntity> GetAllByPredicate(Expression<Func<TaskEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<TaskEntity, DalTask>(Expression.Parameter(typeof(DalTask), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalTask, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            //ToList
            return taskRepository.GetAllByPredicate(exp2).Select(task => task.GetBllEntity()).ToList();
        }

        public void Create(TaskEntity entity)
        {
            entity.IsChecked = false;
            entity.DateCreation = DateTime.Now;
            taskRepository.Create(entity.GetDalEntity());
            uow.Commit();
        }

        public int CreateTask(TaskEntity entity)
        {
            entity.IsChecked = false;
            entity.DateCreation = DateTime.Now;
            int id = taskRepository.CreateTask(entity.GetDalEntity());
            uow.Commit();
            return id;            
        }

        public void MarkAsChecked(TaskEntity entity)
        {
            if (entity.IsChecked == false)
                entity.IsChecked = true;
            else
                entity.IsChecked = false;
            taskRepository.Update(entity.GetDalEntity());
            uow.Commit();
        }

        public void Edit(TaskEntity entity)
        {
            taskRepository.Update(entity.GetDalEntity());
            uow.Commit();
        }

        public void Delete(TaskEntity entity)
        {
            taskRepository.Delete(entity.GetDalEntity());
            uow.Commit();
        }

    }
}
