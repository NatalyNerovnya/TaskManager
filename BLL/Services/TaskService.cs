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
            return taskRepository.GetAll().Select(task => task.GetBllEntity());
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
            return taskRepository.GetAllByPredicate(exp2).Select(task => task.GetBllEntity());
        }

        public void Create(TaskEntity entity)
        {
            entity.IsChecked = false;
            entity.DateCreation = DateTime.Now;
            taskRepository.Create(entity.GetDalEntity());
            uow.Commit();
        }

        //public void Restore(TaskEntity entity)
        //{
        //    entity.IsDelete = false;
        //    taskRepository.Update(entity.GetDalEntity());
        //    uow.Commit();
        //}

        //public void MakeOpen(int id)
        //{
        //    var file = taskRepository.GetOneByPredicate(f => f.Id == id);
        //    file.IsOpen = !(file.IsOpen);
        //    taskRepository.Update(file);
        //    uow.Commit();
        //}

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

        public IEnumerable<TaskEntity> SortTasks(IEnumerable<TaskEntity> tasks, string sortOrder)
        {
            switch (sortOrder)
            {
                case "Checked desc":
                    tasks = tasks.OrderByDescending(t => t.IsChecked);
                    break;
                case "Date":
                    tasks = tasks.OrderBy(t => t.DateCreation);
                    break;
                case "Date desc":
                    tasks = tasks.OrderByDescending(t => t.DateCreation);
                    break;
                default:
                    tasks = tasks.OrderBy(t => t.IsChecked);
                    break;
            }
            return tasks;
        }

    }
}
