using PlugApi.Common;
using PlugApi.Models.Responses.Boards;

namespace PlugApi.Interfaces.Repositories;

public interface IBoardService
{
    /// <summary>
    /// Get all boards in jiraApi.
    /// </summary>
    /// <returns>Get all boards in jiraApi</returns>
    Task<ApiResult<GetAllBoardsResponse>> GetAllBoards();
}
