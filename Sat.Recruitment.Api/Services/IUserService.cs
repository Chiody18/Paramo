using System.Threading.Tasks;
using Sat.Recruitment.Api.Entities;

namespace Sat.Recruitment.Api.Services
{
    public interface IUserService
    {
        Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money);
    }
}