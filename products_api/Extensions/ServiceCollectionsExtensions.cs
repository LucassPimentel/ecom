using Microsoft.Data.SqlClient;
using static products_api.Context.DbContext;

namespace products_api.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddScoped<GetConnection>(sp => async () =>
            {
                var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return connection;
            });

            return builder;
        }
    }
}
