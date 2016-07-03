using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRoleRepository 
    {
        IEnumerable<DalRole> GetAll();

        DalRole GetById(int key);

        IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> f);

        DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> f);

        void Create(DalRole e);

        void Delete(DalRole e);

        void Update(DalRole entity);
    }
}
