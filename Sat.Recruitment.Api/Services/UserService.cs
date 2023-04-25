using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Entities;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>();
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var errors = ValidateInput(name, email, address, phone);
            if (!string.IsNullOrEmpty(errors))
            {
                return new Result() { IsSuccess = false, Errors = errors };
            }

            decimal.TryParse(money, out var moneyDecimal);
            var newUser = new User(name, email, address, phone, userType, money);
            
            LoadUsersFromFile();

            if (IsDuplicateUser(newUser))
            {
                return new Result { IsSuccess = false, Errors = "The user is duplicated" };
            }

            _users.Add(newUser);
            _logger.LogInformation("User Created");

            return new Result { IsSuccess = true, Errors = "User Created" };
        }

        private string ValidateInput(string name, string email, string address, string phone)
        {
            var errors = new StringBuilder();
            if (string.IsNullOrEmpty(name))
                errors.Append("The name is required. ");
            if (string.IsNullOrEmpty(email))
                errors.Append("The email is required. ");
            if (string.IsNullOrEmpty(address))
                errors.Append("The address is required. ");
            if (string.IsNullOrEmpty(phone))
                errors.Append("The phone is required. ");
            return errors.ToString();
        }

        private void LoadUsersFromFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory() + @"\Files\", "users.txt");
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(fileStream);
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                var fields = line.Split(",");
                var user = new User
                {
                    Name = fields[0],
                    Email = fields[1],
                    Phone = fields[2],
                    Address = fields[3],
                    UserType = fields[4],
                    Money = decimal.Parse(fields[5])
                };
                _users.Add(user);
            }
        }

        private bool IsDuplicateUser(User newUser)
        {
            return _users.Any(user => user.Email == newUser.Email || user.Phone == newUser.Phone || user.Name == newUser.Name && user.Address == newUser.Address);
        }
    }
}