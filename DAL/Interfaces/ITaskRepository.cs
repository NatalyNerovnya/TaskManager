using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITaskRepository 
    {
        int CreateTask(DalTask e);

        IEnumerable<DalTask> GetAll();

        DalTask GetById(int key);

        IEnumerable<DalTask> GetAllByPredicate(Expression<Func<DalTask, bool>> f);

        DalTask GetOneByPredicate(Expression<Func<DalTask, bool>> f);

        void Create(DalTask e);

        void Delete(DalTask e);

        void Update(DalTask entity);
    }
}
