using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<DalUser> GetAll();

        DalUser GetById(int key);

        IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> f);

        DalUser GetOneByPredicate(Expression<Func<DalUser, bool>> f);

        void Create(DalUser e);

        void Delete(DalUser e);

        void Update(DalUser entity);
    }
}
