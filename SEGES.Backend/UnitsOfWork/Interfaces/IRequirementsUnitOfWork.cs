using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.UnitsOfWork.Interfaces
{
    public interface IRequirementsUnitOfWork
    {
        Task<ActionResponse<Requirement>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Requirement>>> GetAsync();
    }
}
