namespace Customer.Domain.Abstractions;

public readonly struct ResultT<TValue, TError>
{
    public bool IsError { get; }
    public bool IsSuccess => !IsError;

    private readonly TValue? _value;
    private readonly TError? _error;

    public ResultT(TValue value)
    {
        IsError = false;
        _value = value;
        _error = default;
    }
}