using AutoMapper;
using System.Net;
using System.Text.Json;

namespace PlugApi.Common;

public abstract class HttpClientPlug
{
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;

    protected HttpClientPlug(IMapper mapper, IHttpClientFactory httpClientFactory)
    {
        _mapper = mapper;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ApiResult<TResponse>> DeserializeAsync<TResponse, TModelApi>(HttpResponseMessage httpResponseMessage) where TResponse : class where TModelApi : class
    {
        var ApiResult = new ApiResult<TResponse>();

        ApiResult.StatusCode = httpResponseMessage.StatusCode;

        if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
        {
            using (var httpContentStream = await httpResponseMessage.Content.ReadAsStreamAsync())
            {
                var data = await System.Text.Json.JsonSerializer.DeserializeAsync<TModelApi>(httpContentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                ApiResult.Data = _mapper.Map<TResponse>(data);

                return ApiResult;
            }
        }

        ApiResult.MessageError = await httpResponseMessage.Content.ReadAsStringAsync();

        return ApiResult;
    }
    public async Task<ApiResult<TResponse>> DeserializeAsync<TResponse>(HttpResponseMessage httpResponseMessage) where TResponse : class
    {
        var ApiResult = new ApiResult<TResponse>();

        ApiResult.StatusCode = httpResponseMessage.StatusCode;

        if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
        {
            using (var httpContentStream = await httpResponseMessage.Content.ReadAsStreamAsync())
            {
                ApiResult.Data = await System.Text.Json.JsonSerializer.DeserializeAsync<TResponse>(httpContentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return ApiResult;
            }
        }
        ApiResult.MessageError = await httpResponseMessage.Content.ReadAsStringAsync();

        return ApiResult;
    }
}



