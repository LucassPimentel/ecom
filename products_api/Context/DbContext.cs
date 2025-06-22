using System.Data;

namespace products_api.Context
{
    public class DbContext
    {
        public delegate Task<IDbConnection> GetConnection();
    }
}
