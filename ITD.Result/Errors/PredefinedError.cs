namespace ITD.Result.Errors;
public abstract record PredefinedError
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    public static readonly Error NullValue = new(
        "General.Null",
        "Null value was provided",
        ErrorType.Failure);

    public static Error InvalidInput(string message = "invalid input provided") => new(
        "General.InvalidInput",
        message,
        ErrorType.Failure);

    public static Error NotFound(string message = "not found") => new(
        "General.NotFound",
        message,
        ErrorType.NotFound);

    public static Error Unauthorized(string message = "Unauthorized") => new(
        "General.Unauthorized",
        message,
        ErrorType.Failure);

    public static Error ValidationError(string code = "General.ValidationError", string message = "Validation error occured") => new(
        code,
        message,
        ErrorType.Validation);
}
