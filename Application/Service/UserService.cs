using Application.IService;
using Application.ViewModel;
using Repository.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service
{
    public class UserService : IUserService
    {
        protected readonly IUsersRepository usersRepository;
        public UserService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }
        public async Task<bool> Add(UsersVm entity)
        {
            try
            {
                await usersRepository.Add(entity.ToEntity());
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Delete(UsersVm entity)
        {
            try
            {
                await usersRepository.Delete(entity.Id);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<UsersVm> Get(Guid Id)
        {
            try
            {
                var x = await usersRepository.Get(Id);
                return new UsersVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Mobile = x.Mobile,
                    Address = x.Address,
                    Password = x.Password,
                    UserId = x.UserId,
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<UsersVm>> GetAll()
        {
            try
            {
                var data = (from x in usersRepository.GetAll()
                            select new UsersVm()
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Mobile = x.Mobile,
                                Address = x.Address,
                                Password = x.Password,
                                UserId = x.UserId,
                            }).ToList();
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Tuple<long, List<UsersVm>>> GetAll(int currentPage, int pageSize, UsersVm entity)
        {
            int skip = (currentPage * pageSize);
            var data_qry = (from x in usersRepository.GetAll()
                            select new UsersVm
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Mobile = x.Mobile,
                                Address = x.Address,
                                Password = x.Password,
                                UserId = x.UserId,
                                CreatedAt = x.CreatedAt,
                                CreatedBy = x.CreatedBy,
                                CreatedFrom = x.CreatedFrom,
                                UpdatedAt = x.UpdatedAt,
                                UpdatedBy = x.UpdatedBy,
                                UpdatedFrom = x.UpdatedFrom
                            }).ToList();
            long total = data_qry.Count();
            var data = data_qry.Skip(skip).Take(pageSize).ToList();
            return await Task.FromResult(Tuple.Create(total, data));
        }

        public async Task<bool> Update(UsersVm entity)
        {
            try
            {
                await usersRepository.Update(entity.ToEntity());
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
