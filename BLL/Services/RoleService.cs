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
    public class RoleService : IRoleService
    {

        private readonly IUnitOfWork uow;
        private readonly IRoleRepository roleRepository;

        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
        }

        public IEnumerable<RoleEntity> GetAllEntities()
        {
            return roleRepository.GetAll().Select(role => role.GetBllEntity());
        }

        public RoleEntity GetById(int id)
        {
            return roleRepository.GetById(id).GetBllEntity();
        }

        public IEnumerable<RoleEntity> GetAllByPredicate(Expression<Func<RoleEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<RoleEntity, DalRole>(Expression.Parameter(typeof(DalRole), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalRole, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return roleRepository.GetAllByPredicate(exp2).Select(r => r.GetBllEntity());
        }

        public RoleEntity GetOneByPredicate(Expression<Func<RoleEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<RoleEntity, DalRole>(Expression.Parameter(typeof(DalRole), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalRole, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return roleRepository.GetOneByPredicate(exp2).GetBllEntity();
        }

        public void Create(RoleEntity entity)
        {
            roleRepository.Create(entity.GetDalEntity());
            uow.Commit();
        }

        public void Edit(RoleEntity entity)
        {
            roleRepository.Update(entity.GetDalEntity());
            uow.Commit();
        }

        public void Delete(RoleEntity entity)
        {
            roleRepository.Delete(entity.GetDalEntity());
            uow.Commit();
        }

    }
}
