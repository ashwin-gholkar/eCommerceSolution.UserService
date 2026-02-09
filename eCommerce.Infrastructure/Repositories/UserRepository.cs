using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories
{
    public class UserRepository(DapperDBContext dapperDBContext) : IUserRepository
    {
        private readonly DapperDBContext _dapperDBContext = dapperDBContext;

        public async Task<ApplicationUser?> AddUser(ApplicationUser user)
        {
            //generate new unique user id
            user.UserId = Guid.NewGuid();

            //query to insert user record
            string query = "INSERT INTO public.\"Users\" " +
                           "(\"UserID\", \"Email\", \"PersonName\", \"Gender\", \"Password\") " +
                           "VALUES (@UserID, @Email, @PersonName, @Gender, @Password);";

            int rowCountAffected = await _dapperDBContext.dbConnection
                                    .ExecuteAsync(query, user);

            if (rowCountAffected > 0)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<ApplicationUser?> GetUserBtEmailAndPassword(string? email, string? password)
        {

            string query = "SELECT * FROM public.\"Users\" WHERE \"Email\"= @Email AND \"Password\" = @Password";

            var parameters = new {Email = email, Password = password };

            ApplicationUser? user = await _dapperDBContext.dbConnection.QueryFirstOrDefaultAsync
                <ApplicationUser>(query, parameters);

            return user;
        }
    }
}
