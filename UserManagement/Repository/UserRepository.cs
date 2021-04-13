using Dapper;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Threading.Tasks;
using UserManagement.Dto;

namespace UserManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;

        public UserRepository(IOptions<AppSettings> appSettings)
        {
            _connectionString = appSettings.Value.ConnectionString;
        }

        public async Task CreateUser(User user)
        {

            const string query = "INSERT INTO Users (Id, Name, MobileNumber, Duties)" +
                                  "VALUES(@Id, @Name, @MobileNumber, @Duties)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.QueryFirstOrDefaultAsync(query, new
                {
                    Id = user.Id,
                    Name = user.Name,
                    MobileNumber = user.MobileNumber,
                    Duties = user.Duties.ToString()
                });
            }
        }

        public async Task DeleteUser(string userId)
        {
            const string query = "DELETE FROM Users" +
                                  "WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.QueryFirstOrDefaultAsync(query, new
                {
                    Id = userId
                });
            }
        }

        public async Task<User> GetUser(string userId)
        {
            const string query = "SELECT Id, Name, MobileNumber, Duties FROM Users" +
                                 "WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<User>(query, new
                {
                    Id = userId
                });
            }
        }

        public async Task UpdateUser(User user)
        {
            const string query ="UPDATE Users" +
                                "SET Id = @Id, Name = @Name, MobileNumber = @MobileNumber, Duties = @Duties" +
                                "WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.QueryFirstOrDefaultAsync<User>(query, new
                {
                    Id = user.Id,
                    Name = user.Name,
                    MobileNumber = user.MobileNumber,
                    Duties = user.Duties.ToString()
                });;
            }
        }
    }
}
