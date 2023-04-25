using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly List<User> _users = new List<User>();
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            return await _userService.CreateUser(name, email, address,phone, userType, money);
        }
    }
}
