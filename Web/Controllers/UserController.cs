using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UserController : BaseController
    {
        protected readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> GetUsers()
        {
            var s = await userService.GetAll();
            return this.Json(new
            {
                status = true,
                data= s,
                msg ="data send"
            }); ;
        }
    }
}