using System.Data;
using Dapper;

namespace Infrastructure.Data;
public class DataBaseStandard : IDisposable
{
    protected IDbConnection DbConnection { get; }

    public DataBaseStandard(IDbConnection dbConnection)
    {
        DbConnection = dbConnection;
    }

    public async Task<int> ExecuteCommandAsync(string sql, object paramaters)
    {

        var result = await DbConnection.ExecuteAsync(sql, paramaters)
                                        .ConfigureAwait(false);

        return result;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object paramaters)
    {        

        var result = await DbConnection.QueryAsync<T>(sql, paramaters)
                                        .ConfigureAwait(false);

        return result;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql)
    {

        var result = await DbConnection.QueryAsync<T>(sql)
                                        .ConfigureAwait(false);

        return result;
    }

    public void Dispose()
    {
        DbConnection?.Close();
        GC.SuppressFinalize(this);        
    }

}
