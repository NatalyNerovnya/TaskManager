using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITaskService : IService<TaskEntity>
    {
        IEnumerable<TaskEntity> SortTasks(IEnumerable<TaskEntity> tasks, string sortOrder);

        int CreateTask(TaskEntity taskEntity);
    }
}
