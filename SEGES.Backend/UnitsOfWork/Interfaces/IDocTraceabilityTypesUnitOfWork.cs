using SEGES.Shared.Entities;
using SEGES.Shared.Responses;
using SEGES.Shared.DTOs;

namespace SEGES.Backend.UnitsOfWork.Interfaces
{
    public interface IDocTraceabilityTypesUnitOfWork
    {
        Task<ActionResponse<DocTraceabilityType>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<DocTraceabilityType>>> GetAsync();

        Task<ActionResponse<IEnumerable<DocTraceabilityType>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

    }
}
