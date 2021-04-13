using System.Threading.Tasks;
using UserManagement.Dto;

namespace UserManagement.Repository
{
    public interface IUserRepository
    {
        Task CreateUser(User user);

        Task UpdateUser(User user);

        Task DeleteUser(string userId);

        Task<User> GetUser(string userId);
    }
}
