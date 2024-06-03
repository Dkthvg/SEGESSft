using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories.Interfaces
{
    public interface IGoalsRepository
    {
        Task<ActionResponse<Goal>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Goal>>> GetAsync();
    }
}
