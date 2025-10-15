using FakeDataMcpServer.Models;

namespace FakeDataMcpServer.Validators;

internal static class NumberOfRecordsValidator<T> where T : class
{
    public static Result<T> Validate(int numberOfRecords)
    {
        var result = new Result<T>();
        if (numberOfRecords <= 0)
        {
            result.IsSuccess = false;
            result.Message = "A quantidade de registros deve ser maior que zero.";
        }
        else if (numberOfRecords > 20)
        {
            result.IsSuccess = false;
            result.Message = "A quantidade de registros não pode ser maior que 20.";
        }
        if (!result.IsSuccess.HasValue)
            result.IsSuccess = true;
        return result;
    }
}