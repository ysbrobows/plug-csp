using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlugApi.Common;
using PlugApi.Interfaces;

namespace PlugApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PokemonController : ApiController
{
    private readonly IPokemonService _pokemonService;
    private IMapper _mapper;

    public PokemonController(IPokemonService pokemonService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        : base(httpContextAccessor, httpClientFactory)
    {
        _pokemonService = pokemonService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPokemons()
    {
        try
        {
            var result = await _pokemonService.GetAllPokemons();
            return Response(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}