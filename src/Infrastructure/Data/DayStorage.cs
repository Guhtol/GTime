using System.Data;
using Application.Data;
using Application.SeedWork;

namespace Infrastructure.Data;
public class DayStorage : DataBaseStandard, IDayStorage
{
    public DayStorage(IDbConnection dbConnection) : base(dbConnection)
    {
    }

    public async Task<int> Add(DateTime dateTime)
    {
        const string sql = @"INSERT INTO Day (Date) Values (:dateTime)";
        return await ExecuteCommandAsync(sql, new { dateTime })
                       .ConfigureAwait(false);
    }

    public async Task<IEnumerable<DateTime>> GetAll(DateOnly dateOnly)
    {
        const string sql = @$"Select Date From Day where Date like :date order by Date";
        var date = DateOnlyToDateStringLike(dateOnly);
        return await QueryAsync<DateTime>(sql, new { date }).ConfigureAwait(false);
    }

    public async Task<IEnumerable<DateTime>> GetAllDaiesByMonth(FirstDateOfMonth firstDate, LastDateOfMonth lastDate)
    {
        const string sql = @"Select Date From Day where date(Date)
                        Between :first and :last order by Date";

        return await QueryAsync<DateTime>(sql, new { first = firstDate.DateSqlParameter, last = lastDate.DateSqlParameter });
    }

    public async Task<int> RemoveDay(DateOnly dateOnly)
    {
        const string sql = @"Delete From Day where Date like :date";
        var date = DateOnlyToDateStringLike(dateOnly);
        return await ExecuteCommandAsync(sql, new { date }).ConfigureAwait(false);
    }

    public async Task<int> RemoveHours(DateTime dateTime)
    {
        const string sql = @"Delete From Day where Date like :date";
        var date = DateOnlyToDateTimeStringLike(dateTime);
        return await ExecuteCommandAsync(sql, new { date }).ConfigureAwait(false);
    }

    private static string DateOnlyToDateStringLike(DateOnly dateOnly) => $"{dateOnly:yyyy-MM-dd}%";
    private static string DateOnlyToDateTimeStringLike(DateTime dateOnly) => $"{dateOnly:yyyy-MM-dd HH:mm:ss}%";

}
