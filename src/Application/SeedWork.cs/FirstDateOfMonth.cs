namespace Application.SeedWork;
public readonly struct FirstDateOfMonth
{
    private readonly DateOnly _value;
    private readonly bool _outOfMonth = false;

    private FirstDateOfMonth(int month)
{
        if (month > 12 || month < 1)
        {
            _value = new DateOnly();
            _outOfMonth = true;
            return;
        }

        _value = new DateOnly(DateTime.Now.Year, month, 1);
    }

    public readonly bool OutOfMonth() => _outOfMonth;

    public readonly string DateSqlParameter => $"{_value:yyy-MM-dd}";
    public readonly DateOnly Value => _value;

    public static implicit operator FirstDateOfMonth(int month) => new(month);

}
