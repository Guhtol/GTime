using Application.SeedWork;

namespace Application.Data;
public interface IDayStorage
{
    Task<int> Add(DateTime dateTime);
    Task<IEnumerable<DateTime>> GetAll(DateOnly dateOnly);
    Task<IEnumerable<DateTime>> GetAllDaiesByMonth(FirstDateOfMonth firstDate, LastDateOfMonth lastDate);
    Task<int> RemoveDay(DateOnly value);
    Task<int> RemoveHours(DateTime dateTime);
}
