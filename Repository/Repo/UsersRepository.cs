using Domain;
using Repository.DBContext;
using Repository.IRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repo
{
    public class UsersRepository: Repository<Users, DbContext_Sql>, IUsersRepository
    {
    }
}
