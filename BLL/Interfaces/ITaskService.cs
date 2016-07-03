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
        int CreateTask(TaskEntity taskEntity);
        void MarkAsChecked(TaskEntity taskEntity);
    }
}
