using auth.Interfaces.DbConnectionFactory;
using auth.Interfaces.Repositories;
using auth.Models;
using auth.Resources;
using Dapper;
using System.Threading.Tasks;

namespace auth.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public AuthRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<UserResource> AddUserAsync(User newUser)
        {
            using (var connection = dbConnectionFactory.CreateConnection())
            {
                var parameters = new
                {
                    newUser.UserName,
                    newUser.Email,
                    newUser.PasswordHash,
                    newUser.PasswordSalt
                };

                var insertQuery = @"
                     INSERT INTO [dbo].[Users]
                     ([UserName]
                     ,[Email]
                     ,[PasswordHash]
                     ,[PasswordSalt])
                     OUTPUT INSERTED.Id,
                     		INSERTED.UserName,
                     		INSERTED.Email,
                     		INSERTED.CreatedAt
                     VALUES 
                     (@UserName,
                     @Email,
                     @PasswordHash,
                     @PasswordSalt)
                    ";

                return await connection.QueryFirstAsync<UserResource>(insertQuery, param: parameters);
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using (var connection = dbConnectionFactory.CreateConnection())
            {
                var parameters = new { Email = email };
                var query = @"
                        SELECT Id,
                        UserName,
                        Email,
                        PasswordHash,
                        PasswordSalt,
                        CreatedAt
                        FROM Users
                        WHERE Email = @Email
                        AND IsActive = 1";

                return await connection.QueryFirstOrDefaultAsync<User?>(query, param: parameters);
            }
        }
    }
}
