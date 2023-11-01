namespace Application.SeedWork;
public readonly struct LastDateOfMonth
{
    private readonly DateOnly _value;
    private readonly bool _outOfMonth = false;

    private LastDateOfMonth(int month)
    {
        if (month > 12 || month < 1)
        {
            _value = new DateOnly();
            _outOfMonth = true;
            return;
        }

        var lastDay = DateTime.DaysInMonth(DateTime.Now.Year, month);
        _value = new DateOnly(DateTime.Now.Year,month,lastDay);
    }

    public readonly bool OutOfMonth() => _outOfMonth;
    public readonly string DateSqlParameter => $"{_value:yyy-MM-dd}";
    public readonly DateOnly Value => _value;

    public static implicit operator LastDateOfMonth(int month) => new(month);

}
