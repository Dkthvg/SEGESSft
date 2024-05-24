using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories.Interfaces
{
    public interface IProjectStatusesRepository
    {
        Task<ActionResponse<ProjectStatus>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<ProjectStatus>>> GetAsync();
        Task<ActionResponse<IEnumerable<ProjectStatus>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        Task<IEnumerable<ProjectStatus>> GetComboAsync();
    }
}
