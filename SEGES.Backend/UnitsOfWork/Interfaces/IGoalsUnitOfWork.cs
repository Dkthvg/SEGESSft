using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.UnitsOfWork.Interfaces
{
    public interface IGoalsUnitOfWork
    {
        Task<ActionResponse<Goal>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Goal>>> GetAsync();
    }
}
