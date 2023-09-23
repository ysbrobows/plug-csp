using System.Net;

namespace PlugApi.Common;

public sealed class ApiResult<TResponse>
    where TResponse : class
{
    public HttpStatusCode StatusCode { get; set; }

    public TResponse? Data { get; set; }

    public string? MessageError { get; set; }
}
