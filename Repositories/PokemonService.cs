using AutoMapper;
using PlugApi.Common;
using PlugApi.Interfaces;
using PlugApi.Models.Responses.Boards;
using PlugApi.Models.Responses.Pokemons;

namespace PlugApi.Services;

public class PokemonService : HttpClientPlug, IPokemonService
{
    private readonly IHttpClientFactory _clientFactory;
    public PokemonService(IHttpClientFactory httpClientFactory, IMapper mapper) : base(mapper, httpClientFactory)
    {
        _clientFactory = httpClientFactory;
    }

    public async Task<ApiResult<GetAllPokemonsResponse>> GetAllPokemons()
    {
        var httpClient = _clientFactory.CreateClient("PokemonApi");

        var httpResponseMessage = await httpClient.GetAsync($"/api/v2/pokemon");

        return await DeserializeAsync<GetAllPokemonsResponse>(httpResponseMessage);
    }

    public async Task<ApiResult<GetAllBoardsResponse>> GetAllBoards()
    {
        var httpClient = _clientFactory.CreateClient("JiraApi");
        var httpResponseMessage = await httpClient.GetAsync($"/rest/agile/1.0/board");
        return await DeserializeAsync<GetAllBoardsResponse>(httpResponseMessage);

    }
}
