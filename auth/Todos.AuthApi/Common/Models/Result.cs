namespace Todos.AuthApi.Common.Models;

public class Result
{
    public bool IsSuccess { get; private set; }

    public IEnumerable<string> Errors { get; private set; }

    private Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    private Result(bool isSuccess, IEnumerable<string> errors) : this(isSuccess)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(true);
    }

    public static Result Fail(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}
