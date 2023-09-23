using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlugApi.Common;
using PlugApi.Interfaces.Repositories;
using PlugApi.Models.Responses.Boards;
using Swashbuckle.AspNetCore.Annotations;

namespace PlugApi.Services;

[Route("api/[controller]")]
[SwaggerTag("boards")]
public class BoardService : HttpClientPlug, IBoardService
{
    private readonly IHttpClientFactory _clientFactory;
    public BoardService(IHttpClientFactory httpClientFactory, IMapper mapper) : base(mapper, httpClientFactory)
    {
        _clientFactory = httpClientFactory;
    }

    [HttpGet]
    [Route("allBoards")]
    public async Task<ApiResult<GetAllBoardsResponse>> GetAllBoards()
    {
        var httpClient = _clientFactory.CreateClient("JiraApi");
        var httpResponseMessage = await httpClient.GetAsync($"/rest/agile/1.0/board");
        return await DeserializeAsync<GetAllBoardsResponse>(httpResponseMessage);

    }
}
