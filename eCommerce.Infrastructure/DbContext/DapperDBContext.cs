using AutoMapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.DbContext
{
    public class DapperDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly System.Data.IDbConnection _connection;

        public DapperDBContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            string connectionString = this._configuration
                                      .GetConnectionString("PostgreSQLConnection")!;

            //create connection object using Npgsql library
            _connection = new NpgsqlConnection(connectionString); 
        }
        public IDbConnection dbConnection => _connection;

    }
}
