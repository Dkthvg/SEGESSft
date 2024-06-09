using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.UnitsOfWork.Interfaces
{
    public interface ILearnMoreCommentsUnitOfWork
    {
        Task<ActionResponse<LearnMoreComments>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<LearnMoreComments>>> GetAsync();
    }
}