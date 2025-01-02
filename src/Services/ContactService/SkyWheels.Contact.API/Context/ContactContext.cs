using Microsoft.Data.SqlClient;
using System.Data;

namespace SkyWheels.Contact.API.Context
{
    public class ContactContext
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;

        public ContactContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("connection")!;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
