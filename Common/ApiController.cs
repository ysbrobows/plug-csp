using Microsoft.AspNetCore.Mvc;
using PlugApi.HttpExtensions;

namespace PlugApi.Common;

[ApiController]
public abstract class ApiController : ControllerBase
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    protected ApiController(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClientFactory = httpClientFactory;
    }

    protected new IActionResult Response<TResponse>(ApiResult<TResponse> response)
        where TResponse : class
    {
        CustomResult? result = null;

        if (string.IsNullOrWhiteSpace(response.MessageError))
        {
            var success = response.StatusCode.IsSuccess();

            if (response.Data != null)
                result = new CustomResult(response.StatusCode, success, response.Data);
            else
                result = new CustomResult(response.StatusCode, success);
        }
        else
        {
            var errors = new List<string>();

            if (!string.IsNullOrWhiteSpace(response.MessageError))
                errors.Add(response.MessageError);

            result = new CustomResult(response.StatusCode, false, errors);
        }
        return new JsonResult(result) { StatusCode = (int)result.StatusCode };
    }
}
