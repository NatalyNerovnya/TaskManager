using DAL.DTO;
using DAL.Mappers;
using DAL.Interfaces;
using Helpers;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;

        public RoleRepository(DbContext uow)
        {
            if (uow == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            this.context = uow;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().Select(role => role.GetDalEntity()); 
        }

        public DalRole GetById(int key)
        {
            var ormRole = context.Set<Role>().FirstOrDefault(role => role.Id == key);
            return ormRole == null ? null : ormRole.GetDalEntity();            
        }

        public DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();
        }

        public IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> f)
        {
            var visitor =
                new MyExpressionVisitor<DalRole, Role>(Expression.Parameter(typeof(Role), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<Role, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return context.Set<Role>()
                .Where(exp2)
                .Select(r => r.GetDalEntity());
        }

        public void Create(DalRole e)
        {
            context.Set<Role>().Add(e.GetORMEntity());
        }

        public void Delete(DalRole e)
        {
            context.Set<Role>().Attach(e.GetORMEntity());
            context.Set<Role>().Remove(e.GetORMEntity());
        }

        public void Update(DalRole e)
        {
            
            context.Entry(e.GetORMEntity()).State = EntityState.Modified;
        }
    }
}
