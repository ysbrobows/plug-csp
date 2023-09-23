using System.Net;
using System.Net.Http;

namespace PlugApi.HttpExtensions;

public static class HttpExtensions
{
    public static bool IsSuccess(this HttpStatusCode statusCode) =>
        new HttpResponseMessage(statusCode).IsSuccessStatusCode;
}
