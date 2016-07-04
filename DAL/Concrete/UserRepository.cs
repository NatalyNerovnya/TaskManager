using DAL.DTO;
using DAL.Interfaces;
using DAL.Mappers;
using Helpers;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity.Migrations;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {

        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            if (uow == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            this.context = uow;
        }

        // Should I have Include() there?
        public IEnumerable<DalUser> GetAll()
        {
            var x = context.Set<User>().Include(u => u.Roles).ToList();
            return x.Select(user => user.GetDalEntity());
        }

        public DalUser GetById(int key)
        {
            var ormUser = context.Set<User>().Include(u => u.Roles).FirstOrDefault(u => u.Id == key);
            return ormUser == null ? null : ormUser.GetDalEntity();
        }


        public DalUser GetOneByPredicate(Expression<Func<DalUser, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();
        }

        public IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> f)
        {
            var visitor = new MyExpressionVisitor<DalUser, User>(Expression.Parameter(typeof(User), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<User, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            var x = context.Set<User>().Include(user => user.Roles).Where(exp2).ToList();
            return x.Select(user => user.GetDalEntity());
        }


        public void Create(DalUser e)
        {
            context.Set<User>().Add(e.GetORMEntity());
        }

        public void Delete(DalUser e)
        {
            var user = context.Set<User>().Single(u => u.Id == e.Id);
            var tasksId = context.Set<Task>().Where(t=>t.FromUserId == user.Id || t.ToUserId == user.Id).ToList();
            foreach (var task in tasksId)
            {
               var mis = context.Set<Mission>().Where(m => m.TaskId == task.Id).ToList();
               context.Set<Mission>().RemoveRange(mis);
            }
            context.Set<Task>().RemoveRange(tasksId);
            context.Set<ORM.User>().Remove(e.GetORMEntity());
            context.SaveChanges();
        }

        public void Update(DalUser e)
        {
            context.Set<ORM.User>().AddOrUpdate(e.GetORMEntity());
            context.SaveChanges();
        }

    }
}
