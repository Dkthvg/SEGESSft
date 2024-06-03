using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories.Interfaces
{
    public interface IRequirementsRepository
    {
        Task<ActionResponse<Requirement>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Requirement>>> GetAsync();
    }
}
