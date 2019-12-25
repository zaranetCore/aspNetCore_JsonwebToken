using DotNetCore_Jwt_Server.Models;
using System.Threading.Tasks;

namespace DotNetCore_Jwt_Server.Services
{
    public interface IUserService
    {
        Task<User> LoginAsync(string name, string password);
    }

    public class UserService : IUserService
    {
        private readonly static User User = new User
        {
            Id = 1,
            Name = "beck",
            Password = "123456",
            IsAdmin = true
        };
        public async Task<User> LoginAsync(string name, string password)
        {
            await Task.CompletedTask;

            if (User.Name == name && User.Password == password)
            {
                return User;
            }
            return null;
        }
    }
}
