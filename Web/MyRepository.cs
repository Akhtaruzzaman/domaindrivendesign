using Microsoft.Extensions.DependencyInjection;
using Repository.IRepo;
using Repository.Repo;

namespace Web
{
    public class MyRepository
    {
        public MyRepository(IServiceCollection service)
        {
            service.AddTransient<IUsersRepository, UsersRepository>();
        }
    }
}
