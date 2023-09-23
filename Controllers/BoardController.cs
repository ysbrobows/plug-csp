using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlugApi.Common;
using PlugApi.Interfaces.Repositories;

namespace PlugApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoardController : ApiController
{
    private readonly IBoardService _boardRepository;
    private IMapper _mapper;

    public BoardController(IBoardService boardRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        : base(httpContextAccessor, httpClientFactory)
    {
        _boardRepository = boardRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("allBoards")]
    public async Task<IActionResult> GetAllBoards()
    {
        try
        {
            var result = await _boardRepository.GetAllBoards();
            return Response(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}