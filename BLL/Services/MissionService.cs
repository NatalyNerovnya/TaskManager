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
    public class MissionService : IMissionService
    {
        private readonly IUnitOfWork uow;
        private readonly IMissionRepository missionRepository;

        public MissionService(IUnitOfWork uow, IMissionRepository missionRepository)
        {
            if (uow == null || missionRepository == null)
                throw new ArgumentNullException();
            this.uow = uow;
            this.missionRepository = missionRepository;
        }

        public IEnumerable<MissionEntity> GetAllEntities()
        {
            return missionRepository.GetAll().Select(mission => mission.GetBllEntity());
        }

        public MissionEntity GetById(int id)
        {
            if (id < 0)
                throw new ArgumentException();
            return missionRepository.GetById(id).GetBllEntity();
        }

        public MissionEntity GetOneByPredicate(Expression<Func<MissionEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<MissionEntity, DalMission>(Expression.Parameter(typeof(DalMission), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalMission, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return missionRepository.GetOneByPredicate(exp2).GetBllEntity();
        }

        public IEnumerable<MissionEntity> GetAllByPredicate(Expression<Func<MissionEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<MissionEntity, DalMission>(Expression.Parameter(typeof(DalMission), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalMission, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            var mission = missionRepository.GetAllByPredicate(exp2).ToList();
            return mission.Select(m => m.GetBllEntity());
        }

        public void Create(MissionEntity entity)
        {
            entity.IsDone = false;
            missionRepository.Create(entity.GetDalEntity());
            uow.Commit();
        }

        public void MarkAsDone(MissionEntity entity)
        {
            if (entity.IsDone == false)
                entity.IsDone = true;
            else
                entity.IsDone = false;
            missionRepository.Update(entity.GetDalEntity());
            uow.Commit();
        }

        public void Edit(MissionEntity entity)
        {
            missionRepository.Update(entity.GetDalEntity());
            uow.Commit();
        }

        public void Delete(MissionEntity entity)
        {
            missionRepository.Delete(entity.GetDalEntity());
            uow.Commit();
        }

    }
}
