using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IService;
using Application.Service;
using Microsoft.AspNetCore.Http;

namespace Web
{
    public class MyService
    {
        public MyService(IServiceCollection service)
        {
            service.AddTransient<IUserService, UserService>();
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
