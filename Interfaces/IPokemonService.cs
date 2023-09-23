using PlugApi.Common;
using PlugApi.Models.Responses.Pokemons;

namespace PlugApi.Interfaces;

public interface IPokemonService
{
    /// <summary>
    /// Get all pokemons in api.
    /// </summary>
    /// <returns>Get all pokemons in api</returns>
    Task<ApiResult<GetAllPokemonsResponse>> GetAllPokemons();
}
