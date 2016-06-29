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

        private string path;

        public MissionService(IUnitOfWork uow, IMissionRepository missionRepository, string path)
        {
            this.uow = uow;
            this.missionRepository = missionRepository;
            this.path = path;
        }

        public IEnumerable<MissionEntity> GetAllEntities()
        {
            return missionRepository.GetAll().Select(mission => mission.GetBllEntity());
        }

        public MissionEntity GetById(int id)
        {
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
            var x = missionRepository.GetAllByPredicate(exp2);
            return x.Select(mission => mission.GetBllEntity());
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
            //if (File.Exists(entity.Path))
            //    File.Delete(entity.Path);
            missionRepository.Delete(entity.GetDalEntity());
            uow.Commit();
        }

        //public void AddToCart(MissionEntity entity)
        //{

        //    entity.IsDelete = true;
        //    missionRepository.Update(entity.GetDalEntity());
        //    uow.Commit();
        //}

        //public string RenameFile(int id, string newName)
        //{
        //    var file = missionRepository.GetById(id);
        //    string oldName = file.Name;
        //    var directory = Path.GetDirectoryName(file.Path);
        //    if (directory != null)
        //    {
        //        var newPath = Path.Combine(directory, newName + file.Type);
        //        try
        //        {
        //            if (File.Exists(file.Path))
        //                File.Move(file.Path, newPath);
        //        }
        //        catch
        //        {
        //            return oldName;
        //        }
        //        file.Name = newName;
        //        file.Path = newPath;
        //        missionRepository.Update(file);
        //        uow.Commit();
        //        return newName;
        //    }
        //    return oldName;
        //}
    }
}
