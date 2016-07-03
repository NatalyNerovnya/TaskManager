using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMissionRepository
    {
        IEnumerable<DalMission> GetAll();

        DalMission GetById(int key);

        IEnumerable<DalMission> GetAllByPredicate(Expression<Func<DalMission, bool>> f);

        DalMission GetOneByPredicate(Expression<Func<DalMission, bool>> f);

        void Create(DalMission e);

        void Delete(DalMission e);

        void Update(DalMission entity);
    }
}
