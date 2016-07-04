using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO;
using DAL.Interfaces;
using Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.uow = unitOfWork;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public IEnumerable<UserEntity> GetAllEntities()
        {
            //ToList
            return userRepository.GetAll().Select(u => u.GetBllEntity()).ToList();
        }
        public UserEntity GetById(int id)
        {
            return userRepository.GetById(id).GetBllEntity();
        }

        public IEnumerable<UserEntity> GetAllByPredicate(Expression<Func<UserEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            //ToList()
            return userRepository.GetAllByPredicate(exp2).Select(user => user.GetBllEntity()).ToList();
        }

        public UserEntity GetOneByPredicate(Expression<Func<UserEntity, bool>> f)
        {
            var visitor = new MyExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return userRepository.GetOneByPredicate(exp2).GetBllEntity();
        }

        public void Create(UserEntity user)
        {
            user.Roles = new List<RoleEntity> { roleRepository.GetById(3).GetBllEntity() };
            userRepository.Create(user.GetDalEntity());
            if (!(Directory.Exists(user.Login)))
            {
                try
                {
                    Directory.CreateDirectory(user.Login);
                }
                catch (Exception error)
                {

                }
            }
            uow.Commit();
        }

        public void Edit(UserEntity entity)
        {
            userRepository.Update(entity.GetDalEntity());
            uow.Commit();
        }

        public void Delete(UserEntity entity)
        {
            userRepository.Delete(entity.GetDalEntity());
            uow.Commit();
        }
    }
}
