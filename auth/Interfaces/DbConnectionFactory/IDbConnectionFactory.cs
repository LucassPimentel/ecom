using System.Data;

namespace auth.Interfaces.DbConnectionFactory
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
