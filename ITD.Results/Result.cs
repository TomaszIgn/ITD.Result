using ITD.Results.Errors;
using System.Diagnostics.CodeAnalysis;

namespace ITD.Results;
public class Result
{
    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        Errors = Enumerable.Empty<Error>();
    }

    public Result(bool isSuccess, IEnumerable<Error> errors)
    {
        if (isSuccess && errors.Any(e => e != Error.None) ||
            !isSuccess && errors.All(e => e == Error.None))
        {
            throw new ArgumentException("Invalid error", nameof(errors));
        }
        IsSuccess = isSuccess;
        Errors = errors;
        Error = Error.None;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }
    public IEnumerable<Error> Errors { get; }

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);

    public static Result<TValue> ValidationFailure<TValue>(Error error) =>
        new(default, false, error);

    public static Result<TValue> ValidationFailure<TValue>(IEnumerable<Error> errors) =>
        new(default, false, errors);

    public static Result ValidationFailure(IEnumerable<Error> errors) =>
        new(false, errors);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public Result(TValue? value, bool isSuccess, IEnumerable<Error> errors)
        : base(isSuccess, errors)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}
