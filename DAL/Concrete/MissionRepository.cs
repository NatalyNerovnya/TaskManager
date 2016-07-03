using DAL.DTO;
using DAL.Interfaces;
using ORM;
using Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Mappers;
using System.Data.Entity.Migrations;

namespace DAL.Concrete
{
    public class MissionRepository : IMissionRepository
    {
        private readonly DbContext context;

        public MissionRepository(DbContext uow)
        {
            if (uow == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            this.context = uow;
        }

        public IEnumerable<DalMission> GetAll()
        {
            return context.Set<Mission>().Select(mis => mis.GetDalEntity());
        }

        public DalMission GetById(int key)
        {
            var mis = context.Set<Mission>().FirstOrDefault(f => f.Id == key);
            return mis == null ? null : mis.GetDalEntity();
        }

        public DalMission GetOneByPredicate(Expression<Func<DalMission, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();
        }

        public IEnumerable<DalMission> GetAllByPredicate(Expression<Func<DalMission, bool>> f)
        {
            var visitor = new MyExpressionVisitor<DalMission, Mission>(Expression.Parameter(typeof(Mission), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<Mission, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            var x = context.Set<Mission>().Where(exp2).ToList();
            return x.Select(mis => mis.GetDalEntity());
        }

        public void Create(DalMission dalMission)
        {
            context.Set<Mission>().Add(dalMission.GetORMEntity());
        }

        public void Delete(DalMission dalMission)
        {
            context.Set<Mission>().Attach(dalMission.GetORMEntity());
            context.Set<Mission>().Remove(dalMission.GetORMEntity());
        }

        public void Update(DalMission dalMission)
        {
            context.Set<ORM.Mission>().AddOrUpdate(dalMission.GetORMEntity());
            context.SaveChanges();

            //context.Entry(dalMission.GetORMEntity()).State = EntityState.Modified;
        }
    }
}

