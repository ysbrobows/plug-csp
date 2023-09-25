using System.Net;

namespace PlugApi.Common;

public sealed class ApiResult<TResponse>
    where TResponse : class
{
    public HttpStatusCode StatusCode { get; set; }

    public TResponse? Data { get; set; }

    public string? MessageError { get; set; }
}

public class ApiResult
{

    public HttpStatusCode StatusCode { get; set; }

    public Object? Data { get; set; }

    public string? ErrorMessage { get; set; }

    public ApiResult(HttpStatusCode statusCode, object? data, string? errorMessage)
    {
        StatusCode = statusCode;
        Data = data;
        ErrorMessage = errorMessage;
    }

}
